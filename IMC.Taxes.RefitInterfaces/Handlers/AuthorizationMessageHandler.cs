using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using IMC.Taxes.RefitInterfaces.Options;
using Microsoft.Extensions.Options;

namespace IMC.Taxes.RefitInterfaces.Handlers
{
    // Created this class to add the Header to the refit client
    public class AuthorizationMessageHandler : DelegatingHandler
    {
        private readonly TaxJarOptions _taxJarOptions;
        
        public AuthorizationMessageHandler(IOptions<TaxJarOptions> taxJarOptions)
        {
            _taxJarOptions = taxJarOptions.Value;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancelToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Token", $"token=\"{_taxJarOptions.Token}\"");

            return await base.SendAsync(request, cancelToken);
        }
    }
}
        