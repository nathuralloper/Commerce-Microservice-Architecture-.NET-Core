using Catalog.Persistence.Database;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Service.EventHandlers
{
    public class ProductInStockUpdateStockEventHandler : INotificationHandler<ProductInStockUpdateStockCommand>
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductInStockUpdateStockEventHandler> _logger;
        public ProductInStockUpdateStockEventHandler(
            ApplicationDbContext context,
            ILogger<ProductInStockUpdateStockEventHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(ProductInStockUpdateStockCommand notification, CancellationToken cancellationToken)
        {

            _logger.LogInformation("--- ProductInStockUpdateSotckCommand started");

            var products = notification.Items.Select(x => x.ProductId);
            var stocks = await _context.Stocks.Where(x => products.Contains(x.ProductId)).ToListAsync();

            _logger.LogInformation("--- retrieve products from database");

            foreach (var item in notification.Items)
            {
                var entry = stocks.SingleOrDefault(x => x.ProductId == item.ProductId);

                if (item.Action == Common.Enums.ProductInStockAction.Substract)
                {
                    if (entry == null || item.Stock > entry.Stock)
                    {
                        _logger.LogError($"--- Product {entry.ProductId} - doesn's have enough stock");
                        throw new ProductInStockUpdateStockCommandException($"--- Product {entry.ProductId} - doesn's have enough stock");
                    }
                                        
                    entry.Stock -= item.Stock;
                    _logger.LogInformation($"--- Product {entry.ProductId} - its stock was a subtracted - new stock {entry.Stock}");
                }
                else
                {
                    if (entry == null)
                    {
                        entry = new Domain.ProductInStock
                        {
                            ProductId = item.ProductId
                        };
                        await _context.AddAsync(entry);
                        _logger.LogInformation($"--- New stock record was created for {entry.ProductId} - because didn't exists before");
                    }

                    entry.Stock += item.Stock;
                    _logger.LogInformation($"--- Product {entry.ProductId} - its stock was increased and its new stock is  {entry.Stock}");
                }
            }

            await _context.SaveChangesAsync();

            _logger.LogInformation("--- ProductInStockUpdateSotckCommand ended");
        }
    }
}
