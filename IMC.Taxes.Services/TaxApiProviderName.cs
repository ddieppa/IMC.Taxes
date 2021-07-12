using System.Collections.Generic;
using IMC.Taxes.Contracts.Helpers;

namespace IMC.Taxes.Services
{
    public class TaxApiProviderName : Enumeration
    {
        public static TaxApiProviderName TaxJarX = new TaxApiProviderName(1, nameof(TaxJarX));
        public TaxApiProviderName(int id, string name) : base(id, name)
        {
        }
        
        public static IEnumerable<TaxApiProviderName> List() =>
            new[] { TaxJarX };
    }
}