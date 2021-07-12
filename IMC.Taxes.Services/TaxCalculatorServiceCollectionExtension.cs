using IMC.Taxes.Services.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace IMC.Taxes.Services
{
    public static class TaxCalculatorServiceCollectionExtension
    {
        public static IServiceCollection AddTaxCalculatorServices(this IServiceCollection services)
        {
            services.AddTransient<ITaxCalculatorProvider, TaxJarCalculatorProvider>();
            services.AddTransient<ITaxCalculatorProviderFactory, TaxCalculatorProviderFactory>();
            return services;
        }
    }
}