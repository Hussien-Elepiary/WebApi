using ECommerce_Demo_Core.Entities.Order_Aggregate;
using ECommerce_Demo_Core.Entities.Products;
using ECommerce_Demo_Core.IRepositories;
using ECommerce_Demo_Core.IServices;
using ECommerce_Demo_Core.Repositories;
using ECommerce_Demo_Core.Specifications.OrderSpec;
using ECommerce_Demo_Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Service.Order_Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IPaymentService paymentService)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentService = paymentService;
        }

        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items) 
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var productItemOrder = new ProductItemOrdered(item.Id,product.Name,product.PictureUrl);

                var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);

                orderItems.Add(orderItem);
            }

            var subTotal = orderItems.Sum(item => item.Cost * item.Quantity);

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var orderSpecs = new OrderByPaymentIntentId(basket.PaymentIntentID);

            var existingOrder = await _unitOfWork.Repository<Order>().GetWithSpecAsync(orderSpecs);

            if (existingOrder != null) {
                _unitOfWork.Repository<Order>().Delete(existingOrder);

                await _paymentService.CreateOrUpdatePaymentIntent(basket.Id);
            }
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal, basket.PaymentIntentID);

            await _unitOfWork.Repository<Order>().AddAsync(order);
                
            var result =  await _unitOfWork.Complete();

            if (result > 0) return order;
            return null;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMetodSpecss(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderWithItemsAndDeliveryMetodSpecss(buyerEmail, orderId); 
            var order = await _unitOfWork.Repository<Order>().GetWithSpecAsync(spec);
            if (order is null)  return null; 
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return deliveryMethods;
        }
    }
}
