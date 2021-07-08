using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IMC.Taxes.Api.Models;
using IMC.Taxes.Api.Serialization;

namespace IMC.Taxes.Api.Services
{
    public class TaxJarService: ITaxService
    {
        private readonly HttpClient _httpClient;

        public TaxJarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Tax> GetTaxesByLocationAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Tax> GetOrderTaxesAsync(Order order)
        {
            var uri = "https://api.taxjar.com/v2/taxes";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new SnakeCaseNamingPolicy()
            };
            
            var httpResponseMessage = await _httpClient.PostAsJsonAsync(uri, order, options);
            httpResponseMessage.EnsureSuccessStatusCode();
            
            var root = await httpResponseMessage.Content.ReadFromJsonAsync<TaxRoot>(options);
            
            return root?.Tax;

        }
    }
}