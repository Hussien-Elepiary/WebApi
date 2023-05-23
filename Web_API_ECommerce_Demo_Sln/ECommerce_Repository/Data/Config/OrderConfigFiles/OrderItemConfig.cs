using ECommerce_Demo_Core.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_Repository.Data.Config.OrderConfigFiles
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(OItem => OItem.Product,Product => Product.WithOwner());

            builder.Property(OItem => OItem.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
