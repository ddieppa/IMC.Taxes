using System;
using System.Net.Http;
using IMC.Taxes.RefitInterfaces.Handlers;
using IMC.Taxes.RefitInterfaces.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using Refit;

namespace IMC.Taxes.RefitInterfaces
{
    public static class RefitServiceCollectionExtension
    {
        public static IServiceCollection AddRefitServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var taxJarOptions = new TaxJarOptions();
            var taxJarSection = configuration.GetSection(TaxJarOptions.TaxJar);
            // Had to add TaxJarOptions here in the configuration cuz will be used to grab
            // these options from another classes
            services.Configure<TaxJarOptions>(taxJarSection);
            // Had to do this bind in order to use the options here in the Configuration
            taxJarSection.Bind(taxJarOptions);

            // Added this way cuz is only used here in the configuration
            var waitAnRetryConfig = new WaitAndRetryOptions();
            configuration.GetSection(WaitAndRetryOptions.WaitAndRetry).Bind(waitAnRetryConfig);
            
            // Add some Polly retry policies
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(waitAnRetryConfig.Retry,
                    _ => TimeSpan.FromMilliseconds(waitAnRetryConfig.Wait));
            
            // Add some Polly timeout policy
            var timeoutPolicy = Policy
                .TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(waitAnRetryConfig.Timeout));

            // This is needed to add this class to later be used in the refit client
            services.AddTransient<AuthorizationMessageHandler>();

            services.AddRefitClient<ITaxJarApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(taxJarOptions.Url))
                .AddHttpMessageHandler<AuthorizationMessageHandler>()
                .AddPolicyHandler(retryPolicy)
                .AddPolicyHandler(timeoutPolicy);

            return services;
        }
    }
}