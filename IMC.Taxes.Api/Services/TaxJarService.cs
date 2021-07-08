using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using IMC.Taxes.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

            var settings = new JsonSerializerSettings
            {
                ContractResolver     = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() },
                DefaultValueHandling = DefaultValueHandling.Include,
                TypeNameHandling     = TypeNameHandling.None,
                NullValueHandling    = NullValueHandling.Ignore,
                Formatting           = Formatting.None,
                ConstructorHandling  = ConstructorHandling.AllowNonPublicDefaultConstructor
            };

            var orderJson = JsonConvert.SerializeObject(order, settings);
            var orderContent = new StringContent(orderJson, Encoding.UTF8, MediaTypeNames.Application.Json);
            // _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            // _httpClient.DefaultRequestHeaders.Add("Authorization", "Token token=\"5da2f821eee4035db4771edab942a4cc\"");
            var httpResponseMessage = await _httpClient.PostAsync(uri, orderContent);
            httpResponseMessage.EnsureSuccessStatusCode();
            var responseJson = await httpResponseMessage.Content.ReadAsStringAsync();
            var taxResult = JsonConvert.DeserializeObject<Tax>(responseJson, settings);
            return taxResult;

        }
    }
}