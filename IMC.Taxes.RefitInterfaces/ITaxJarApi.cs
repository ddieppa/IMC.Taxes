
using System.Threading.Tasks;
using IMC.Taxes.Contracts.Responses;
using IMC.Taxes.RefitInterfaces.QueryParams;
using Refit;

namespace IMC.Taxes.RefitInterfaces
{
    public interface ITaxJarApi
    {
        [Get("/rates/{zip}")]
        Task<RateRootResponse> GetTaskRateForLocation(string zip, RateQueryParam queryParams);
    }
}