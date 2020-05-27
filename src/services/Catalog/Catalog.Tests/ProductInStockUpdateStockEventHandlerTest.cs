using Catalog.Service.EventHandlers;
using Catalog.Service.EventHandlers.Commands;
using Catalog.Service.EventHandlers.Exceptions;
using Catalog.Tests.Config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.Tests
{
    [TestClass]
    public class ProductInStockUpdateStockEventHandlerTest
    {
        private ILogger<ProductInStockUpdateStockEventHandler> GetLogger
        {
            get
            {
                return new Mock<ILogger<ProductInStockUpdateStockEventHandler>>().Object;
            }
        }


        [TestMethod]
        public void TryToSubstractStockWhenProductHasStock()
        {
            var _context = ApplicationDbContextInMemory.Get();

            int productId = 1;
            int productInStockId = 1;
            double stock = 1;

            _context.Stocks.Add(new Domain.ProductInStock
            {
                ProductId = productId,
                ProductInStockId = productInStockId,
                Stock = stock
            });

            _context.SaveChanges();

            var handler = new ProductInStockUpdateStockEventHandler(_context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem{
                        ProductId = productId,
                        Stock = stock,
                        Action = Common.Enums.ProductInStockAction.Substract }
                }
            }, new System.Threading.CancellationToken()).Wait();


        }


        [TestMethod]
        [ExpectedException(typeof(ProductInStockUpdateStockCommandException))]
        public void TryToSubstractStockWhenProductHasntStock()
        {
            var _context = ApplicationDbContextInMemory.Get();

            int productId = 6;
            int productInStockId = 6;
            double stock = 1;

            _context.Stocks.Add(new Domain.ProductInStock
            {
                ProductId = productId,
                ProductInStockId = productInStockId,
                Stock = stock
            });

            _context.SaveChanges();
            try
            {
                var handler = new ProductInStockUpdateStockEventHandler(_context, GetLogger);

                handler.Handle(new ProductInStockUpdateStockCommand
                {
                    Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem{
                        ProductId = productId,
                        Stock = 3,
                        Action = Common.Enums.ProductInStockAction.Substract }
                }
                }, new System.Threading.CancellationToken()).Wait();

            }
            catch (AggregateException ae)
            {
                var exception = ae.GetBaseException();
                if (exception is ProductInStockUpdateStockCommandException)
                    throw new ProductInStockUpdateStockCommandException(exception?.InnerException?.Message);
            }

        }

        [TestMethod]
        public void TryToAddStockWhenProductExists()
        {
            var _context = ApplicationDbContextInMemory.Get();

            int productId = 5;
            int productInStockId = 5;
            double stock = 1;

            _context.Stocks.Add(new Domain.ProductInStock
            {
                ProductId = productId,
                ProductInStockId = productInStockId,
                Stock = stock
            });

            _context.SaveChanges();

            var handler = new ProductInStockUpdateStockEventHandler(_context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem{
                        ProductId = productId,
                        Stock = 2,
                        Action = Common.Enums.ProductInStockAction.Add }
                }
            }, new System.Threading.CancellationToken()).Wait();

            var stockInDb = _context.Stocks.Single(x => x.ProductId == productId).Stock;
            Assert.AreEqual(stockInDb, 3);


        }

        [TestMethod]
        public void TryToAddStockWhenProductNotExists()
        {
            var _context = ApplicationDbContextInMemory.Get();

            int productId = 4;
                      
            var handler = new ProductInStockUpdateStockEventHandler(_context, GetLogger);

            handler.Handle(new ProductInStockUpdateStockCommand
            {
                Items = new List<ProductInStockUpdateItem>()
                {
                    new ProductInStockUpdateItem{
                        ProductId = productId,
                        Stock = 2,
                        Action = Common.Enums.ProductInStockAction.Add }
                }
            }, new System.Threading.CancellationToken()).Wait();

            var stockInDb = _context.Stocks.Single(x => x.ProductId == productId).Stock;
            Assert.AreEqual(stockInDb, 2);


        }

    }
}
