using System;
using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Net.Mime;
using IMC.Taxes.Api.Models;
using IMC.Taxes.Api.Refit;
using IMC.Taxes.Api.Serialization;
// using IMC.Taxes.Api.Services;
using IMC.Taxes.RefitInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using Polly.Timeout;
using Refit;

namespace IMC.Taxes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var taxJarOptions = new TaxJarOptions();
            var taxJarSection = Configuration.GetSection(TaxJarOptions.TaxJar);
            services.Configure<TaxJarOptions>(taxJarSection);
          
            taxJarSection.Bind(taxJarOptions);
            
            // services.AddHttpClient<ITaxService, TaxJarService>(client =>
            // {
            //     var url = taxJarSection.GetValue<string>(nameof(TaxJarOptions.Url));
            //     var token = taxJarSection.GetValue<string>(nameof(TaxJarOptions.Token));
            //     client.BaseAddress = new Uri(url);
            //     client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            //     client.DefaultRequestHeaders.Authorization =
            //         new AuthenticationHeaderValue("Token", $"token=\"{token}\"");
            // });
            
            services.AddTransient<AuthorizationMessageHandler>();

            // var waitAnRetryConfig = Configuration.GetSection(nameof(WaitAndRetryConfig));
            // services.Configure<WaitAndRetryConfig>(waitAnRetryConfig);
            // var retry = waitAnRetryConfig.GetValue<int>(nameof(WaitAndRetryConfig.Retry));
            // var wait = waitAnRetryConfig.GetValue<int>(nameof(WaitAndRetryConfig.Wait));
            // var timeOut = waitAnRetryConfig.GetValue<int>(nameof(WaitAndRetryConfig.Timeout));

            var waitAnRetryConfig = new WaitAndRetryConfig();
            Configuration.GetSection(nameof(WaitAndRetryConfig)).Bind(waitAnRetryConfig);
            
            
            var retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>() 
                .WaitAndRetryAsync(waitAnRetryConfig.Retry, _ => TimeSpan.FromMilliseconds(waitAnRetryConfig.Wait));

            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(waitAnRetryConfig.Timeout));

            services.AddRefitClient<ITaxJarApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(taxJarOptions.Url))
                .AddHttpMessageHandler<AuthorizationMessageHandler>();
                // .AddPolicyHandler(retryPolicy)
                // .AddPolicyHandler(timeoutPolicy); 
            
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMC.Taxes.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMC.Taxes.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}