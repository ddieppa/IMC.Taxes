using FluentValidation;
using IMC.Taxes.Contracts.Requests;

namespace IMC.Taxes.Services.Validators
{
    // shipping and to_country are required based on the TaxJar api documentation
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {
            RuleFor(orderRequest => orderRequest.Shipping)
                .NotEqual(default(float)).WithMessage(orderRequest => $"{nameof(orderRequest.Shipping)} has to be greater than 0");
            RuleFor(orderRequest => orderRequest.ToCountry)
                .NotEmpty().WithMessage(orderRequest => $"Please ensure you have entered {nameof(orderRequest.ToCountry)}");
        }
    }
}