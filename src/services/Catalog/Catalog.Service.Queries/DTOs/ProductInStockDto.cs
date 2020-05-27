using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.Queries.Service
{
    public class ProductInStockDto
    {
        public int ProductInStockId { get; set; }
        public int ProductId { get; set; }
        public double Stock { get; set; }
    }
}
