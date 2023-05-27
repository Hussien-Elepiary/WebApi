using AutoMapper;
using ECommerce_Demo_Core.Entities.Order_Aggregate;
using ECommerce_Demo_Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Web_API_ECommerce_Demo.Dtos.BasketDtos;
using Web_API_ECommerce_Demo.Errors;

namespace Web_API_ECommerce_Demo.Controllers.Payment
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [ProducesResponseType(typeof(CustomerBasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null) return NotFound(new ApiResponse(404,"Basket was not Found"));

            return Ok(basket);  
        }

        const string endpointSecret = "whsec_7414833d2356571620dcdc582630277fa283420b2dadb38f13a2a777dfc5ad40";

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

            Order order; 

            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, true);
                    //_logger.LogInformation("Payment Succeeded", paymentIntent.Id);
                    break;
                case Events.PaymentIntentPaymentFailed:
                    order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, false);
                    //_logger.LogInformation("Payment Failed", paymentIntent.Id);
                    break;
            }

            return new EmptyResult();
        }
    }
}
