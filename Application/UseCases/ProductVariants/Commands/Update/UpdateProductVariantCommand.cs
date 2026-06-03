using MediatR;

namespace FarmaCorp.Application.UseCases.ProductVariants.Commands.Update;

public record UpdateProductVariantCommand(
    int ProductVariantId,
    decimal Price,
    string Sku,
    decimal? Concentration,
    int? UnitId,
    bool IsActive // Regla: "estado de variante"
) : IRequest<bool>;