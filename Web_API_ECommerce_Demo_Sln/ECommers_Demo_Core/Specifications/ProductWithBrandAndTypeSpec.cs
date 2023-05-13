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
        public ProductWithBrandAndTypeSpec()
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }
        public ProductWithBrandAndTypeSpec(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductType);
            Includes.Add(P => P.ProductBrand);
        }

    }
}
