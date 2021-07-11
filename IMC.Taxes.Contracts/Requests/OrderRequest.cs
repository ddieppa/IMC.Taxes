﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IMC.Taxes.Contracts.Requests
{
    public class OrderRequest
    {
        [JsonPropertyName("from_country")]
        public string FromCountry { get; set; }

        [JsonPropertyName("from_zip")]
        public string FromZip { get; set; }

        [JsonPropertyName("from_state")]
        public string FromState { get; set; }

        [JsonPropertyName("to_country")]
        public string ToCountry { get; set; }

        [JsonPropertyName("to_zip")]
        public string ToZip { get; set; }

        [JsonPropertyName("to_state")]
        public string ToState { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("shipping")]
        public double Shipping { get; set; }

        [JsonPropertyName("line_items")]
        public List<LineItemRequest> LineItems { get; set; }
    }


}