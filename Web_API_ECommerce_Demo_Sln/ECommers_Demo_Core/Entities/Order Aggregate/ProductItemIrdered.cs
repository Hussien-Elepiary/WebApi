namespace ECommerce_Demo_Core.Entities.Order_Aggregate
{
    public class ProductItemIrdered
    {
        public ProductItemIrdered()
        {
            
        }
        public ProductItemIrdered(int productID, string productName, string picUrl)
        {
            ProductID = productID;
            ProductName = productName;
            PicUrl = picUrl;
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string PicUrl { get; set; }
    }
}