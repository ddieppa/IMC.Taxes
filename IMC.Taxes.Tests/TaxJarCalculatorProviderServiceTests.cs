using System;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using IMC.Taxes.Contracts.Requests;
using IMC.Taxes.Contracts.Responses;
using IMC.Taxes.RefitInterfaces;
using IMC.Taxes.Services;
using IMC.Taxes.Services.Validators;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace IMC.Taxes.Tests
{
    public class TaxJarCalculatorProviderServiceTests
    {
        private readonly TaxJarCalculatorProviderService _sut; // system under test
        private readonly ITaxJarApi _taxJarApi = Substitute.For<ITaxJarApi>();
        private readonly IValidator<OrderRequest> _validator = new OrderRequestValidator();
        private readonly IFixture _fixture = new Fixture();

        public TaxJarCalculatorProviderServiceTests()
        {
            _sut = new TaxJarCalculatorProviderService(_taxJarApi, _validator);
        }

        [Fact]
        public async Task GetSalesTaxForAnOrderAsync_WhenAllIsValid_ShouldReturnResults()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                FromCountry = null,
                FromZip = null,
                FromState = null,
                ToCountry = "US", //required
                ToZip = null,
                ToState = null,
                Amount = 0,
                Shipping = 1.5, //required
                LineItems = null
            };

            var apiResponse = _fixture.Create<TaxRootResponse>();

            _taxJarApi.GetSalesTaxForAnOrderAsync(orderRequest).Returns(apiResponse);
            // Act
            var result = await _sut.GetSalesTaxForAnOrderAsync(orderRequest);
            // Assert
            result.Should().BeEquivalentTo(apiResponse);
        }
        
        [Fact]
        public async Task GetSalesTaxForAnOrderAsync_WhenNoToCountry_ShouldThrowException()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                Shipping = 1.5, //required
            };
            // Act
            Func<Task> act = async () => await _sut.GetSalesTaxForAnOrderAsync(orderRequest);
            // Assert
            act
                .Should().Throw<TaxCalculatorException>()
                .WithInnerException<ValidationException>()
                .WithMessage("*ToCountry*");
        }
    }
}