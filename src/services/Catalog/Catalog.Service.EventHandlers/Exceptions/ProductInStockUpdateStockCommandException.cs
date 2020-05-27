using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Service.EventHandlers.Exceptions
{
    public class ProductInStockUpdateStockCommandException : Exception
    {
        public ProductInStockUpdateStockCommandException(string message)
            : base(message)
        {

        }
    }
}
