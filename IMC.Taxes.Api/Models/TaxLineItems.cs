namespace IMC.Taxes.Api.Models
{
    public class TaxLineItems
    {
        public float CityAmount { get; set; }
        public float CityTaxRate { get; set; }
        public float CityTaxableAmount { get; set; }
        public float CombinedTaxRate { get; set; }
        public float CountyAmount { get; set; }
        public float CountyTaxRate { get; set; }
        public float CountyTaxableAmount { get; set; }
        public string Id { get; set; }
        public float SpecialDistrictAmount { get; set; }
        public float SpecialDistrictTaxableAmount { get; set; }
        public float SpecialTaxRate { get; set; }
        public float StateAmount { get; set; }
        public float StateSalesTaxRate { get; set; }
        public float StateTaxableAmount { get; set; }
        public float TaxCollectable { get; set; }
        public float TaxableAmount { get; set; }
    }

}
