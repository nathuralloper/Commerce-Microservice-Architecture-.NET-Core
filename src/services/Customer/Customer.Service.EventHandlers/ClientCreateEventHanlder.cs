using Customer.Domain;
using Customer.Persistence.Database;
using Customer.Service.EventHandlers.Commands;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Customer.Service.EventHandlers
{
    public class ClientCreateEventHanlder : INotificationHandler<ClientCreateCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClientCreateEventHanlder> _logger;
        public ClientCreateEventHanlder(ApplicationDbContext context, ILogger<ClientCreateEventHanlder> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(ClientCreateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                await _context.AddAsync(new Client
                {
                    Name = command.Name,
                });
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ups error when trying to save the client, Detail: {ex.Message}");
            }
        }
    }
}
