using AutoMapper;
using ECommerce_Demo_Core.Entities.Order_Aggregate;
using ECommerce_Demo_Core.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Security.Claims;
using Web_API_ECommerce_Demo.Dtos.AuthDtos;
using Web_API_ECommerce_Demo.Dtos.Orders;
using Web_API_ECommerce_Demo.Errors;

namespace Web_API_ECommerce_Demo.Controllers.Bussines
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var shippingAddress = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BaskitId, orderDto.DeliveryMethodId, shippingAddress);
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);

            if (mappedOrder is null) return BadRequest(new ApiResponse(400));
            return Ok(mappedOrder);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);
            var mappedOrder = _mapper.Map<IReadOnlyList<OrderToReturnDto>> (orders);
            return Ok(mappedOrder);
        }

        [ProducesResponseType(typeof(OrderToReturnDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int orderId)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForUserAsync(email, orderId);
            var mappedOrder = _mapper.Map<OrderToReturnDto>(order);

            if (mappedOrder is null) return NotFound(new ApiResponse(404));
            return Ok(mappedOrder);
        }

        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods() 
        {
            var deliveryMethod = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethod);
        }

    }
}
