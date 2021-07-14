using FluentValidation;
using IMC.Taxes.Contracts;
using IMC.Taxes.Contracts.Requests;
using IMC.Taxes.RefitInterfaces;
using Microsoft.Extensions.DependencyInjection;


namespace IMC.Taxes.Services.Factories
{
    public interface ITaxCalculatorProviderFactory
    {
        ITaxCalculatorProviderService CreateTaxCalculatorProvider(string taxCalculatorProviderName);
        // ITaxCalculatorProvider CreateTaxCalculatorProvider(TaxApiProviderName taxApiProviderName);
    }

    public class TaxCalculatorProviderFactory : ITaxCalculatorProviderFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TaxCalculatorProviderFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        public ITaxCalculatorProviderService CreateTaxCalculatorProvider(string taxApiProviderName)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            ITaxCalculatorProviderService taxCalculatorProviderService = null;
            
            switch (taxApiProviderName)
            {
                case Constants.TaxCalculatorProviderNames.TaxJar:
                    var taxJarApiInstance = serviceProvider.GetRequiredService<ITaxJarApi>();
                    var validator = serviceProvider.GetRequiredService<IValidator<OrderRequest>>();
                    taxCalculatorProviderService = new TaxJarCalculatorProviderService(taxJarApiInstance, validator);
                    break;
                default:
                    break;
            }

            return taxCalculatorProviderService;
        }

       
    }
}