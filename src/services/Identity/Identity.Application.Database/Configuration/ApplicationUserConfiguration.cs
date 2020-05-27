using Identity.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Application.Database.Configuration
{
    public class ApplicationUserConfiguration
    {
        public ApplicationUserConfiguration(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(128);

            builder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(e => e.UserId).IsRequired();

        }
    }

}
