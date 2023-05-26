using ECommerce_Demo_Core.Entities.Order_Aggregate;

namespace Web_API_ECommerce_Demo.Dtos.Orders
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set;}
        public string Status { get; set; }
        public Address ShippingAddress { get; set; }
        public DeliveryMethodDto DeliveryMethod { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

    }
}
