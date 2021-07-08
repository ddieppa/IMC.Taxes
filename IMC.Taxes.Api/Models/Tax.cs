namespace IMC.Taxes.Api.Models
{
    public class Tax
    {
        public float AmountToCollect { get; set; }
        public Breakdown Breakdown { get; set; }
        public bool FreightTaxable { get; set; }
        public bool HasNexus { get; set; }
        public Jurisdictions Jurisdictions { get; set; }
        public float OrderTotalAmount { get; set; }
        public float Rate { get; set; }
        public float Shipping { get; set; }
        public string TaxSource { get; set; }
        public float TaxableAmount { get; set; }
    }

}
