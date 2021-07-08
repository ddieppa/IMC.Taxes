using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using IMC.Taxes.Api.Models;
using IMC.Taxes.Api.Serialization;
using Microsoft.Extensions.Options;

namespace IMC.Taxes.Api.Services
{
    public class TaxJarService: ITaxService
    {
        private readonly HttpClient _httpClient;
        private readonly TaxJarOptions _taxJarOptions;

        public TaxJarService(HttpClient httpClient, IOptions<TaxJarOptions> taxJarOptions)
        {
            _httpClient = httpClient;
            _taxJarOptions = taxJarOptions.Value;
        }
        public async Task<Tax> GetTaxesByLocationAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Tax> GetOrderTaxesAsync(Order order)
        {
            var uri = _taxJarOptions.Url;

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