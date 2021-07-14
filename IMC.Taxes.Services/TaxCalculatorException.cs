using System;
using System.Runtime.Serialization;
using Refit;

namespace IMC.Taxes.Services
{
    public class TaxCalculatorException: Exception
    {
        public TaxCalculatorException(ApiException ex) : base($"{ex.Message} @{ex.HttpMethod.Method}('{ex.Uri?.AbsoluteUri}') Content: '{ex.Content}'", ex)
        {
        }

        protected TaxCalculatorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public TaxCalculatorException(string? message) : base(message)
        {
        }

        public TaxCalculatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        public TaxCalculatorException()
        {
        }
    }
}