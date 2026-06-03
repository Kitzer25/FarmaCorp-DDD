using MediatR;

namespace FarmaCorp.Application.UseCases.ProductImages.Commands.Create;

public record CreateProductImageCommand(
    int? ProductId,
    int? ProductVariantId,
    string ImageUrl,
    string AltText,
    bool IsMain
) : IRequest<int>;