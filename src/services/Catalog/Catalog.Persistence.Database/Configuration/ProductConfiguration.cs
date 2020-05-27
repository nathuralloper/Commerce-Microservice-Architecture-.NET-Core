using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Persistence.Database.Configuration
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> builder)
        {
            builder.HasIndex(x => x.ProductId);
            builder.Property(x => x.ProductId).IsRequired(true);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(64);
            builder.Property(x => x.Description).IsRequired(true).HasMaxLength(256);
            builder.Property(x => x.Price).IsRequired(true);

            //Product by default...
            List<Product> products = new List<Product>();
            Random random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                products.Add(new Product
                {
                    ProductId = i,
                    Name = $"Product {i}",
                    Description = $"Description {i}",
                    Price = random.Next(100, 1000)                   
                });
            }

            builder.HasData(products);

        }
    }
}
