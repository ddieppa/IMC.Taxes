namespace IMC.Taxes.Api.Models
{
    public class OrderLineItems
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string ProductTaxCode { get; set; }
    }


}
