using System.Text.Json.Serialization;

namespace IMC.Taxes.Contracts.Responses
{
    public class TaxRootResponse
    {
        [JsonPropertyName("tax")]
        public TaxResponse TaxResponse { get; set; }
    }
}