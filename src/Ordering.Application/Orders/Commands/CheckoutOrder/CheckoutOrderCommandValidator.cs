using FluentValidation;

namespace Ordering.Application.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(e => e.Username)
            .NotEmpty().WithMessage("{Username} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{Username} must not exceed 50 characters.");

        RuleFor(e => e.EmailAddress)
            .NotEmpty().WithMessage("{EmailAddress} is required.");

        RuleFor(e => e.TotalPrice)
            .NotEmpty().WithMessage("{TotalPrice} is required.")
            .GreaterThan(0).WithMessage("{TotalPrice} should be greater then 0.");
    }
}
