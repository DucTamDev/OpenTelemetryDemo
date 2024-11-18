using Contact.Interfaces.HttpServices.Product;
using Contact.Interfaces.HttpServices.Product.Dtos;
using OpenTelemetry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.HttpServices.ProductApi
{
    public class ProductApi : IProductApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        public ProductApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IList<ProductDto>> GetAllProduct()
        {
            ActivitySource source = new("InsightSource");
            using (Activity activity = source.StartActivity("Infrastructure.HttpServices", ActivityKind.Internal))
            {
                activity?.SetTag("client.name", "Product");
                activity?.SetTag("method.name", "GetAllProduct");
                activity?.AddBaggage("user.userId", "0012");
                activity?.AddBaggage("proxy.ip", "192.168.1.100");

                var httpClient = _httpClientFactory.CreateClient();
                using (var response = await httpClient.GetAsync("https://localhost:44385/api/product/all", HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    var result = await JsonSerializer.DeserializeAsync<List<ProductDto>>(stream, _options);

                    return result ?? new List<ProductDto>();
                }
            }
        }

        public Task<ProductDto> GetProductById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
