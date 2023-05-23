using ECommerce_Demo_Core.Entities.Order_Aggregate;
using ECommerce_Demo_Core.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce_Repository.Data
{
    public static class storeContextSeed
	{
		public static async Task SeedAsync(StoreContext context)
		{
            if (!context.ProductBrands.Any())
            {
				var brandsData = File.ReadAllText("../ECommerce_Repository/Data/DataSeed/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				if (brands?.Count > 0)
				{
					foreach (var brand in brands)
						await context.Set<ProductBrand>().AddAsync(brand);

					await context.SaveChangesAsync();
				}
			}

			if (!context.ProductTypes.Any())
			{
				var typesData = File.ReadAllText("../ECommerce_Repository/Data/DataSeed/types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

				if (types?.Count > 0)
				{
					foreach (var type in types)
						await context.Set<ProductType>().AddAsync(type);

					await context.SaveChangesAsync();
				}
			}

			if (!context.Products.Any())
			{
				var productsData = File.ReadAllText("../ECommerce_Repository/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productsData);

				if (products?.Count > 0)
				{
					foreach (var brand in products)
						await context.Set<Product>().AddAsync(brand);

					await context.SaveChangesAsync();
				}
			}

            if (!context.DeliveryMethods.Any())
            {
                var deliveryMethod = File.ReadAllText("../ECommerce_Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethod);

                if (deliveryMethods?.Count > 0)
                {
                    foreach (var dMethod in deliveryMethods)
                        await context.Set<DeliveryMethod>().AddAsync(dMethod);

                    await context.SaveChangesAsync();
                }
            }
        }
	}
}
