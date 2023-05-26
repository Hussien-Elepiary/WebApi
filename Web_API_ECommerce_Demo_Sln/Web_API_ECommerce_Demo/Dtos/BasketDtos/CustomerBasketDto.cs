using ECommerce_Demo_Core.Entities.Cart;
using System.ComponentModel.DataAnnotations;

namespace Web_API_ECommerce_Demo.Dtos.BasketDtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
        public string? PaymentIntentID { get; set; }
        public string? ClientSecret { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal? ShippingCost { get; set; }
    }
}
