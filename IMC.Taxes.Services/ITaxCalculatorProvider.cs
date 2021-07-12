using System.Threading.Tasks;
using IMC.Taxes.Contracts.QueryParams;
using IMC.Taxes.Contracts.Requests;
using IMC.Taxes.Contracts.Responses;

namespace IMC.Taxes.Services
{
    public interface ITaxCalculatorProvider
    {
        Task<RateRootResponse> GetTaskRateForLocation(string zip, RateQueryParam queryParams);
        Task<TaxRootResponse> GetSalesTaxForAnOrderAsync(OrderRequest order);
    }
}