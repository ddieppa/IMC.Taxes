using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IMC.Taxes.Contracts.Responses
{
    public class BreakdownResponse
    {
        [JsonPropertyName("city_tax_collectable")]
        public double CityTaxCollectable { get; set; }

        [JsonPropertyName("city_tax_rate")]
        public double CityTaxRate { get; set; }

        [JsonPropertyName("city_taxable_amount")]
        public double CityTaxableAmount { get; set; }

        [JsonPropertyName("combined_tax_rate")]
        public double CombinedTaxRate { get; set; }

        [JsonPropertyName("county_tax_collectable")]
        public double CountyTaxCollectable { get; set; }

        [JsonPropertyName("county_tax_rate")]
        public double CountyTaxRate { get; set; }

        [JsonPropertyName("county_taxable_amount")]
        public double CountyTaxableAmount { get; set; }

        [JsonPropertyName("line_items")]
        public List<LineItemResponse> LineItems { get; } = new List<LineItemResponse>();

        [JsonPropertyName("shipping")]
        public ShippingResponse ShippingResponse { get; set; }

        [JsonPropertyName("special_district_tax_collectable")]
        public double SpecialDistrictTaxCollectable { get; set; }

        [JsonPropertyName("special_district_taxable_amount")]
        public double SpecialDistrictTaxableAmount { get; set; }

        [JsonPropertyName("special_tax_rate")]
        public double SpecialTaxRate { get; set; }

        [JsonPropertyName("state_tax_collectable")]
        public double StateTaxCollectable { get; set; }

        [JsonPropertyName("state_tax_rate")]
        public double StateTaxRate { get; set; }

        [JsonPropertyName("state_taxable_amount")]
        public double StateTaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public double TaxCollectable { get; set; }

        [JsonPropertyName("taxable_amount")]
        public double TaxableAmount { get; set; }
    }
}