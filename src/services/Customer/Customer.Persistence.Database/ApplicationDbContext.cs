using Customer.Domain;
using Customer.Persistence.Database.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Customer.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Customer");

            ModelConfig(builder);

        }

        private void ModelConfig(ModelBuilder builder)
        {
            new ClientConfiguration(builder.Entity<Client>());
        }

    }
}
