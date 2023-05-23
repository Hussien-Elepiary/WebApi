namespace Web_API_ECommerce_Demo.Dtos.Orders
{
    public class OrderItemDto
    {
        public int id { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string PicUrl { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }

    }
}
