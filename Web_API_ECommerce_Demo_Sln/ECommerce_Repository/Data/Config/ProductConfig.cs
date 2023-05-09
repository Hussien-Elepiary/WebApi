using ECommerce_Demo_Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Repository.Data.Config
{


	internal class ProductConfig : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			//builder.Property(P => P.Id) .IsRequired();
			builder.Property(P => P.Name)/*.IsRequired()*/.HasMaxLength(100);
			builder.Property(P => P.Description).IsRequired();
			builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
			builder.Property(P => P.PicUrl).IsRequired();


			//Each Product has one Barnd and One Type But Each Brand and Type Have many Products
			builder.HasOne(P => P.ProductBrand).WithMany();
			builder.HasOne(P=>P.ProductType).WithMany();
		}
	}
}
