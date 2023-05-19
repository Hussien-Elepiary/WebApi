using ECommerce_Demo_Core.Entities.Products;
using ECommerce_Repository.Data.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Repository.Data
{
    public class StoreContext : DbContext
	{
        public StoreContext(DbContextOptions<StoreContext> options) : base(options){}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//modelBuilder.ApplyConfiguration(new ProductConfig());
			// this line will Get all the Classes that Impliment interFace (IEntityTypeConfiguration) Using Refliction
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
    }
}
