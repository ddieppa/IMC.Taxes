using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMC.Taxes.Api.Models;
using IMC.Taxes.Api.Services;
using IMC.Taxes.Contracts.Responses;
using IMC.Taxes.RefitInterfaces;
using IMC.Taxes.RefitInterfaces.QueryParams;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace IMC.Taxes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly ITaxJarApi _taxJarApi;

        public TaxCalculatorController(ITaxJarApi taxJarApi)
        {
            _taxJarApi = taxJarApi;
        }
        // GET: api/TaxCalculator
        [HttpGet]
        public async Task<RateResponse> Get()
        {
            // var orderLineItemList = new List<OrderLineItems>
            // {
            //     new OrderLineItems
            //     {
            //         Quantity = 1,
            //         UnitPrice = 15.0,
            //         ProductTaxCode = "31000"
            //     }
            // };
            // var orderDto = new Order
            // {
            //     FromCountry = "US",
            //     FromZip = "07001",
            //     FromState = "NJ",
            //     ToCountry = "US",
            //     ToZip = "07446",
            //     ToState = "NJ",
            //     Amount = 16.50,
            //     Shipping = 1.5,
            //     LineItems = orderLineItemList.ToArray()
            // };
            // var result = await _taxService.GetOrderTaxesAsync(orderDto);
            // return result;
            
            var queryParams = new RateQueryParam()
            {
                Street = "312 Hurricane Lane",
                City = "Williston",
                State = "VT",
                Country = "US"
            };
            var response = await _taxJarApi.GetTaskRateForLocation("90404", queryParams);
            
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
        public async Task<Tax> Post([FromBody] Order order)
        {
            // var result = await  _taxJarApi.GetOrderTaxesAsync(order);
            // return result;
            return null;
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
