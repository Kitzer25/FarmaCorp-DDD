using MediatR;

namespace FarmaCorp.Application.UseCases.ProductVariants.Commands.Create;

public record CreateProductVariantCommand(
    int ProductId,
    int DrugFormId,
    int? UnitId,
    decimal? Concentration,
    int PackageSize,
    string PackageDescription,
    string Sku,
    decimal Price,
    decimal? CompareAtPrice
) : IRequest<int>; // Devuelve el ID de la nueva variante