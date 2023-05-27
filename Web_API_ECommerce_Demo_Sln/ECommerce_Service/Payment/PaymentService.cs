using ECommerce_Demo_Core.Entities.Cart;
using ECommerce_Demo_Core.Entities.Order_Aggregate;
using ECommerce_Demo_Core.Entities.Products;
using ECommerce_Demo_Core.IRepositories;
using ECommerce_Demo_Core.IServices;
using ECommerce_Demo_Core.Specifications.OrderSpec;
using ECommerce_Demo_Core.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = ECommerce_Demo_Core.Entities.Products.Product;

namespace ECommerce_Service.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket == null) return null;

            //get shippingPrice
            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                basket.ShippingCost = deliveryMethod.Cost;
                shippingPrice = deliveryMethod.Cost;
            }

            if(basket?.Items?.Count > 0)
            {
                foreach ( var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    if(item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }

                }
            }

            var service = new PaymentIntentService();
            
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)(basket.Items.Sum(item => item.Price * item.Quantity * 100) + basket.ShippingCost * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else 
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(basket.Items.Sum(item => item.Price * item.Quantity * 100) + basket.ShippingCost * 100),
                };
                await service.UpdateAsync(basket.PaymentIntentId,options);
            }

            await _basketRepository.UdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded)
        {
            var spec = new OrderByPaymentIntentId(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);

            if (isSucceeded) order.Status = OrderStatus.PaymentReceivd;
            else order.Status = OrderStatus.PaymentFaild;

            _unitOfWork.Repository<Order>().Update(order);

            await _unitOfWork.CompleteAsync();

            return order;
        }
    }
}
