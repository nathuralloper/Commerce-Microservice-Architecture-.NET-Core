using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Persistence.Database.Configuration
{
    public class OrderDetailConfiguration
    {
        public OrderDetailConfiguration(EntityTypeBuilder<Order.Domain.OrderDetail> builder)
        {
            builder.HasIndex(x => x.OrderDetailId);
            builder.Property(x => x.ProductId).IsRequired(true);
            builder.Property(x => x.Quantity).IsRequired(true);
            builder.Property(x => x.UnitPrice).IsRequired(true);
            builder.Property(x => x.Total).IsRequired(true);            
        }
    }
}
