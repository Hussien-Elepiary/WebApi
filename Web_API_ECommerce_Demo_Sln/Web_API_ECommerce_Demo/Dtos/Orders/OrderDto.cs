using Web_API_ECommerce_Demo.Dtos.AuthDtos;

namespace Web_API_ECommerce_Demo.Dtos.Orders
{
    public class OrderDto
    {
        public string BaskitId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
