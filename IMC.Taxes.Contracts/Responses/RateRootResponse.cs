using System.Text.Json.Serialization;

namespace IMC.Taxes.Contracts.Responses
{
    public class RateRootResponse
    {
        [JsonPropertyName("rate")]
        public RateResponse RateResponse { get; set; }
    }
}