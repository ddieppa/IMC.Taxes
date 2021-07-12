using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMC.Taxes.Api.Models;
using IMC.Taxes.Api.Services;
using IMC.Taxes.Contracts.QueryParams;
using IMC.Taxes.Contracts.Requests;
using IMC.Taxes.Contracts.Responses;
using IMC.Taxes.RefitInterfaces;
using IMC.Taxes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace IMC.Taxes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxCalculatorProvider _taxCalculatorProvider;

        public TaxCalculatorController(ITaxCalculatorProvider taxCalculatorService)
        {
            _taxCalculatorProvider = taxCalculatorService;
        }
        // GET: api/TaxCalculator
        [HttpGet]
        public async Task<RateResponse> Get()
        {
            var queryParams = new RateQueryParam()
            {
                Street = "312 Hurricane Lane",
                City = "Williston",
                State = "VT",
                Country = "US"
            };
            var response = await _taxCalculatorProvider.GetTaskRateForLocation("90404", queryParams);
            
            return response.RateResponse;
        }

        // GET: api/TaxCalculator/5
        [HttpGet("{zip}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TaxCalculator
        [HttpPost]
        public async Task<TaxResponse> Post([FromBody] OrderRequest order)
        {
            // var result = await  _taxJarApi.GetOrderTaxesAsync(order);
            // return result;

            var result = await _taxCalculatorProvider.GetSalesTaxForAnOrderAsync(order);
            return result.TaxResponse;
        }

        // PUT: api/TaxCalculator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/TaxCalculator/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
