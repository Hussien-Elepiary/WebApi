namespace ECommerce_Demo_Core.Entities.Order_Aggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int productID, string productName, string picUrl)
        {
            ProductID = productID;
            ProductName = productName;
            PictureUrl = picUrl;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}