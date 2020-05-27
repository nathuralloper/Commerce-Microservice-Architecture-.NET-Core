
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Options;
using Order.Service.Proxies.Catalog.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Service.Proxies.Catalog
{
   
    public class CatalogQueueProxy : ICatalogProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly string _connectionString;

        public CatalogQueueProxy(IOptions<AzureServiceBus> azure)
        {
            _connectionString = azure.Value.ConnectionString;
        }
        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {
            var queueClient = new QueueClient(_connectionString, "");
                        
            //Serialize message
            string body = JsonSerializer.Serialize(command);
            var messages = new Message(Encoding.UTF8.GetBytes(body));

            //send the message to the queue
            await queueClient.SendAsync(messages);

            //close
            await queueClient.CloseAsync();
        }
    }
}
