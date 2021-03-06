
using System.Threading.Tasks;
using IMC.Taxes.Contracts.QueryParams;
using IMC.Taxes.Contracts.Requests;
using IMC.Taxes.Contracts.Responses;
using Refit;

namespace IMC.Taxes.RefitInterfaces
{
    public interface ITaxJarApi
    {
        [Get("/rates/{zip}")]
        Task<RateRootResponse> GetTaskRateForLocation(string zip, RateQueryParam queryParams);
        
        [Post("/taxes")]
        Task<TaxRootResponse> GetSalesTaxForAnOrderAsync(OrderRequest order);
    }
}