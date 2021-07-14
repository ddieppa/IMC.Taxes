using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using IMC.Taxes.Contracts.Requests;
using IMC.Taxes.Services.Validators;
using Xunit;

namespace IMC.Taxes.Tests
{
    public class OrderRequestValidatorTest
    {
        private readonly OrderRequestValidator _orderRequestValidator;

        public OrderRequestValidatorTest()
        {
            _orderRequestValidator = new OrderRequestValidator();
        }

        [Fact]
        public async Task Shipping_IsZero_ShouldThrowError()
        {
            var orderRequest = new OrderRequest();

            var result = await _orderRequestValidator.TestValidateAsync(orderRequest);

            result.ShouldHaveValidationErrorFor(x => x.Shipping)
                .WithSeverity(Severity.Error);
        }
        
        [Fact]
        public async Task ToCountry_IsEmpty_ShouldThrowError()
        {
            var orderRequest = new OrderRequest();

            var result = await _orderRequestValidator.TestValidateAsync(orderRequest);

            result.ShouldHaveValidationErrorFor(x => x.ToCountry)
                .WithSeverity(Severity.Error);
        }
    }
}