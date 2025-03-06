using FluentValidation;

namespace Business.Products.Commands.Update;

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .ScalePrecision(2, 18).WithMessage("Price must have up to 18 digits in total, with 2 decimals.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .MaximumLength(500).WithMessage("Product description must be at most 500 characters.");
    }
}