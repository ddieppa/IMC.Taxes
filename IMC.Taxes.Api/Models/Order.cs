namespace IMC.Taxes.Api.Models
{

    public class Order
    {
        public string FromStreet { get; set; }
        public string FromCity { get; set; }
        public string FromState { get; set; }
        public string FromZip { get; set; }
        public string FromCountry { get; set; }
        public OrderLineItems[] LineItems { get; set; }
        public string ToStreet { get; set; }
        public string ToCity { get; set; }
        public string ToState { get; set; }
        public string ToZip { get; set; }
        public string ToCountry { get; set; }
        public double Amount { get; set; }
        public double Shipping { get; set; }
    }


}
