using FluentValidation;

namespace FarmaCorp.Application.UseCases.ProductVariants.Commands.Update;

public class UpdateProductVariantCommandValidator : AbstractValidator<UpdateProductVariantCommand>
{
    public UpdateProductVariantCommandValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El precio actualizado no puede ser negativo.");
            
        RuleFor(x => x.Sku).NotEmpty();
    }
}