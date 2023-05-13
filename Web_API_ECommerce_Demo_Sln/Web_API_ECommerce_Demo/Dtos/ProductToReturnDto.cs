using ECommerce_Demo_Core.Entities;

namespace Web_API_ECommerce_Demo.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicUrl { get; set; }
        public decimal Price { get; set; }
        public int ProductBrandId { get; set; } // Foreign Key 
        public string ProductBrand { get; set; } // Nav prop {one}
        public int ProductTypeId { get; set; } // Foreign Key
        public string ProductType { get; set; } // Nav prop {one}
    }
}
