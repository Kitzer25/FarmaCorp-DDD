using FluentValidation;

namespace FarmaCorp.Application.UseCases.ProductVariants.Commands.Create;

public class CreateProductVariantCommandValidator : AbstractValidator<CreateProductVariantCommand>
{
    public CreateProductVariantCommandValidator()
    {
        // Regla: Precio >= 0
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El precio de la variante no puede ser negativo.");

        RuleFor(x => x.Sku)
            .NotEmpty()
            .WithMessage("El SKU es obligatorio para crear la variante.");
    }
}