using MediatR;
using Microsoft.Extensions.Logging;
using Order.Domain;
using Order.Persistence.Database;
using Order.Service.EventHandlers.Commands;
using Order.Service.Proxies.Catalog;
using Order.Service.Proxies.Catalog.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Service.EventHandlers
{
    public class OrderCreateEventHandler : INotificationHandler<OrderCreateCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderCreateEventHandler> _logger;
        private readonly ICatalogProxy _catalogProxy;
        public OrderCreateEventHandler(ApplicationDbContext context,
            ILogger<OrderCreateEventHandler> logger,
            ICatalogProxy catalogProxy)
        {
            _context = context;
            _logger = logger;
            _catalogProxy = catalogProxy;
        }

        public async Task Handle(OrderCreateCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("--- New order creation started");
            var entry = new Domain.Order();

            using (var trx = await _context.Database.BeginTransactionAsync())
            {
                //01. prepare detail
                _logger.LogInformation("--- Preparing detail");
                PrepareDetail(entry, command);

                //02. prepare header
                _logger.LogInformation("--- Preparing header");
                PrepareHeader(entry, command);

                //03. create order
                _logger.LogInformation("--- Creating order");
                await _context.AddAsync(entry);
                await _context.SaveChangesAsync();


                _logger.LogInformation($"--- Order {entry.OrderId} was created");

                //04. Update stocks
                try
                {
                    await _catalogProxy.UpdateStockAsync(new ProductInStockUpdateStockCommand
                    {
                        Items = command.Items.Select(x => new ProductInStockUpdateItem
                        {
                            Action = ProductInStockAction.Substract,
                            ProductId = x.ProductId,
                            Stock = x.Quantity
                        })
                    });
                    _logger.LogInformation("--- Updating stock");
                }
                catch (Exception ex)
                {
                    _logger.LogError("--- Ups error when updating stock... Detail: " + ex.Message);
                    throw new Exception("");
                }


                _logger.LogInformation("--- Updating stock");

                //Logica para actualizar el stock
                await trx.CommitAsync();
            }
        }

        private void PrepareHeader(Domain.Order entry, OrderCreateCommand command)
        {
            entry.Status = Common.Enums.OrderStatus.Pending;
            entry.PaymentType = command.PaymentType;
            entry.ClientId = command.ClientId;
            entry.CreateAt = DateTime.UtcNow;

            entry.Total = entry.Items.Sum(x => x.Total);
        }

        private void PrepareDetail(Domain.Order entry, OrderCreateCommand command)
        {
            entry.Items = command.Items.Select(x => new OrderDetail
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Total = x.Price * x.Quantity,
                UnitPrice = x.Price
            }).ToList();
        }
    }
}
