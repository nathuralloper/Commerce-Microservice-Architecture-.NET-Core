
using Api.Gateways.Models;
using Api.Gateways.Models.Catalog.DTOs;
using Api.Gateways.Proxies.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateways.Proxies
{
    public interface ICatalogProxy
    {
        Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> IdProducts);
        Task<ProductDto> GetAsync(int id);
    }

    public class CatalogProxy : ICatalogProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CatalogProxy(HttpClient httpClient,
            IOptions<ApiUrls> apiUrls,
            IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _apiUrls = apiUrls.Value;
        }

        public async Task<DataCollection<ProductDto>> GetAllAsync(int page, int take, IEnumerable<int> idProducts)
        {
            var ids = string.Join(',', idProducts ?? new List<int>());

            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogUrl}v1/products?page={page}&take={take}&ids={idProducts}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ProductDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ProductDto> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CatalogUrl}v1/products/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ProductDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }


    }
}
