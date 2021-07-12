using IMC.Taxes.Contracts;
using IMC.Taxes.RefitInterfaces;
using Microsoft.Extensions.DependencyInjection;


namespace IMC.Taxes.Services.Factories
{
    public interface ITaxCalculatorProviderFactory
    {
        ITaxCalculatorProvider CreateTaxCalculatorProvider(string taxCalculatorProviderName);
        // ITaxCalculatorProvider CreateTaxCalculatorProvider(TaxApiProviderName taxApiProviderName);
    }

    public class TaxCalculatorProviderFactory : ITaxCalculatorProviderFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TaxCalculatorProviderFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public ITaxCalculatorProvider CreateTaxCalculatorProvider(string taxApiProviderName)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            ITaxCalculatorProvider taxCalculatorProvider = null;
            
            switch (taxApiProviderName)
            {
                case Constants.TaxCalculatorProviderNames.TaxJar:
                    var taxJarApiInstance = serviceProvider.GetRequiredService<ITaxJarApi>();
                    taxCalculatorProvider = new TaxJarCalculatorProvider(taxJarApiInstance);
                    break;
                default:
                    break;
            }

            return taxCalculatorProvider;
        }

       
    }
}