using ECommerce_Demo_Core.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Specifications.Products
{
    public class productBaseConstructor: BaseSpecification<Product>
    {
        public productBaseConstructor(ProductSpecParams productSpec)
            : base(
                    P =>
                        (string.IsNullOrEmpty(productSpec.Search) || P.Name.ToLower().Contains(productSpec.Search)) &&
                        (!productSpec.BrandId.HasValue || P.ProductBrandId == productSpec.BrandId.Value) &&
                        (!productSpec.TypeId.HasValue || P.ProductTypeId == productSpec.TypeId.Value)
                 )
        {

        }

        public productBaseConstructor(int id) : base(p => p.Id == id) { }
    }
}
