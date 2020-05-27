using Customer.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Persistence.Database.Configuration
{
    public class ClientConfiguration
    {
        public ClientConfiguration(EntityTypeBuilder<Client> builder)
        {
            builder.HasIndex(x => x.ClientId);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(128);

            var _clients = new List<Client>();
            for (int i = 1; i <= 10; i++)
            {
                _clients.Add(new Client { ClientId = i, Name = $"Juan Jose Gonzales Mejias {i}" });
            }

            builder.HasData(_clients);

        }
    }
}
