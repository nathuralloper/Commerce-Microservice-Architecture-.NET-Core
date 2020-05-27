using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Persistence.Database.Configuration
{
    public class ProductInStockConfiguration
    {
        public ProductInStockConfiguration(EntityTypeBuilder<ProductInStock> builder)
        {
            builder.HasIndex(x => x.ProductInStockId);
            builder.Property(x => x.ProductId).IsRequired(true);

            //Product in stock by default...
            List<ProductInStock> productInStocks = new List<ProductInStock>();
            Random random = new Random();

            for (int i = 1; i <= 10; i++)
            {
                productInStocks.Add(new ProductInStock
                {
                    ProductInStockId = i,
                    ProductId = i,
                    Stock = random.Next(0, 50)
                });
            }

            builder.HasData(productInStocks);
        }
    }
}
