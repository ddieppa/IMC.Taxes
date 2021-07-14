using FluentValidation;
using IMC.Taxes.Services.Factories;
using IMC.Taxes.Services.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace IMC.Taxes.Services
{
    public static class TaxCalculatorServiceCollectionExtension
    {
        public static IServiceCollection AddTaxCalculatorServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<OrderRequestValidator>(ServiceLifetime.Transient);
            services.AddTransient<ITaxCalculatorProviderService, TaxJarCalculatorProviderService>();
            services.AddTransient<ITaxCalculatorProviderFactory, TaxCalculatorProviderFactory>();
            return services;
        }
    }
}