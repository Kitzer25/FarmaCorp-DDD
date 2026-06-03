using FluentValidation;

namespace FarmaCorp.Application.UseCases.ProductImages.Commands.Create;

public class CreateProductImageCommandValidator : AbstractValidator<CreateProductImageCommand>
{
    public CreateProductImageCommandValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("La URL de la imagen es obligatoria.");

        RuleFor(x => x)
            .Must(x => (x.ProductId.HasValue && !x.ProductVariantId.HasValue) || 
                       (!x.ProductId.HasValue && x.ProductVariantId.HasValue))
            .WithMessage("La imagen debe pertenecer a un producto genérico o a una variante, pero no a ambos.");
    }
}