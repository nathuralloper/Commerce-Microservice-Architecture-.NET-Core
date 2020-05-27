using Microsoft.EntityFrameworkCore;
using Order.Domain;
using Order.Persistence.Database.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
        }

        public DbSet<Order.Domain.Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Order");

            ConfigModel(builder);

        }

        private void ConfigModel(ModelBuilder builder)
        {
            new Orderconfiguration(builder.Entity<Order.Domain.Order>());
            new OrderDetailConfiguration(builder.Entity<Order.Domain.OrderDetail>());
        }

    }
}
