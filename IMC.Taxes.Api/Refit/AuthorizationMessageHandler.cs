using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using IMC.Taxes.Api.Models;
using Microsoft.Extensions.Options;

namespace IMC.Taxes.Api.Refit
{
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        public readonly TaxJarOptions _taxJarOptions;
        
        public AuthorizationMessageHandler(IOptions<TaxJarOptions> taxJarOptions)
        {
            _taxJarOptions = taxJarOptions.Value;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancelToken)
        {
            // request.Headers.Authorization = new AuthenticationHeaderValue("Token", $"token=\"5da2f821eee4035db4771edab942a4cc\"");
            request.Headers.Authorization = new AuthenticationHeaderValue("Token", $"token=\"{_taxJarOptions.Token}\"");

            return await base.SendAsync(request, cancelToken);
        }
    }
}
        