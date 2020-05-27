using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.Proxies.Catalog.Commands
{
    public interface ICatalogProxy
    {
        Task UpdateStockAsync(ProductInStockUpdateStockCommand command);
    }
}
