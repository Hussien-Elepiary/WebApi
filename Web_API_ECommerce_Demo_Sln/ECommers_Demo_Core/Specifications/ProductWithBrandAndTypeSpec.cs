using ECommerce_Demo_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Demo_Core.Specifications
{
    public class ProductWithBrandAndTypeSpec:BaseSpecification<Product>
    {
        /// <summary>
        /// this Constructor just Get all product includes list
        /// </summary>
        public ProductWithBrandAndTypeSpec(string? sort, int? brandId,int? typeId)
            :base(
                    P => 
                        (!brandId.HasValue || P.ProductBrandId == brandId.Value) &&
                        (!typeId.HasValue  || P.ProductTypeId ==  typeId.Value)
                 )

        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            } 
                else
            {
                AddOrderBy(P => P.Name);
            }
        }
        public ProductWithBrandAndTypeSpec(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }

    }
}
