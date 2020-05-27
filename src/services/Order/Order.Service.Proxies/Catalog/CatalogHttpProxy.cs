
using Microsoft.AspNetCore.Http;
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
    
    public class CatalogHttpProxy : ICatalogProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CatalogHttpProxy(IOptions<ApiUrls> apiUrls,
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _apiUrls = apiUrls.Value;
            _httpClient = httpClient;
        }
        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json");

            var request = await _httpClient.PutAsync(_apiUrls.CatalogUrl + "v1/stocks", content);

            //en caso que no devuelva un 200:ok este metodo realize una excepcion
            request.EnsureSuccessStatusCode();
        }
    }
}
