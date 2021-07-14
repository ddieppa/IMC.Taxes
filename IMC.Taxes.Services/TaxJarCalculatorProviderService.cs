using System;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using IMC.Taxes.Contracts.QueryParams;
using IMC.Taxes.Contracts.Requests;
using IMC.Taxes.Contracts.Responses;
using IMC.Taxes.RefitInterfaces;
using Polly.Timeout;
using Refit;

namespace IMC.Taxes.Services
{
    public class TaxJarCalculatorProviderService : ITaxCalculatorProviderService
    {
        private readonly ITaxJarApi _taxJarApi;
        private readonly IValidator<OrderRequest> _validator;

        public TaxJarCalculatorProviderService(ITaxJarApi taxJarApi, IValidator<OrderRequest> validator)
        {
            _taxJarApi = taxJarApi;
            _validator = validator;
        }

        public async Task<RateRootResponse> GetTaskRateForLocation(string zip, RateQueryParam queryParams)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(zip))
                {
                    throw new ArgumentException("Have to enter a ZipCode");
                }

                return await _taxJarApi.GetTaskRateForLocation(zip, queryParams);
            }
            catch (ArgumentException ex)
            {
                throw new TaxCalculatorException(ex.Message);
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
                await _validator.ValidateAndThrowAsync(order);
                return await _taxJarApi.GetSalesTaxForAnOrderAsync(order);
            }
            catch (ValidationException ex)
            {
                var errorMessages = string.Join(",", ex.Errors.Select(x => x.ErrorMessage));
                throw new TaxCalculatorException(errorMessages, ex);
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