using Identity.Application.Database.Configuration;
using Identity.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Identity.Application.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<ApplicationUserRole>

            builder.HasDefaultSchema("Identity");

            ModelConfig(builder);
        }

        private void ModelConfig(ModelBuilder builder)
        {
            new ApplicationUserConfiguration(builder.Entity<ApplicationUser>());
            new ApplicationRoleConfiguration(builder.Entity<ApplicationRole>());
        }

    }
}
