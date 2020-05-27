using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Persistence.Database.Configuration
{
    public class Orderconfiguration
    {
        public Orderconfiguration(EntityTypeBuilder<Order.Domain.Order> builder)
        {
            builder.HasIndex(x => x.OrderId);
            builder.Property(x => x.ClientId).IsRequired(true);
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.PaymentType).IsRequired(true);
            builder.Property(x => x.CreateAt).IsRequired(true);
            builder.Property(x => x.Total).IsRequired(true);
        }
    }
}
