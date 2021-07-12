using System;
using System.Threading.Tasks;
using IMC.Taxes.Contracts.QueryParams;
using IMC.Taxes.Contracts.Requests;
using IMC.Taxes.Contracts.Responses;
using IMC.Taxes.RefitInterfaces;
using Polly.Timeout;
using Refit;

namespace IMC.Taxes.Services
{
    class TaxJarCalculatorProvider : ITaxCalculatorProvider
    {
        private readonly ITaxJarApi _taxJarApi;

        public TaxJarCalculatorProvider(ITaxJarApi taxJarApi)
        {
            _taxJarApi = taxJarApi;
        }

        public async Task<RateRootResponse> GetTaskRateForLocation(string zip, RateQueryParam queryParams)
        {
            try
            {
                return await _taxJarApi.GetTaskRateForLocation(zip, queryParams);
            }
            catch (ApiException ex)
            {
                throw new TaxCalculatorException(ex);
            }
            catch (Exception ex) when (ex is TimeoutRejectedException)
            {
                // TimeoutRejectedException: Thrown by Polly TimeoutPolicy.

                throw new TaxCalculatorException(ex.Message, ex);
            }
        }

        public async Task<TaxRootResponse> GetSalesTaxForAnOrderAsync(OrderRequest order)
        {
            try
            {
                return await _taxJarApi.GetSalesTaxForAnOrderAsync(order);
            }
            catch (ApiException ex)
            {
                throw new TaxCalculatorException(ex);
            }
            catch (Exception ex) when (ex is TimeoutRejectedException)
            {
                // TimeoutRejectedException: Thrown by Polly TimeoutPolicy.

                throw new TaxCalculatorException(ex.Message, ex);
            }
        }
    }
}