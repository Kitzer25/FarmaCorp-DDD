using Domain.DTO_s.ProductImage;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.ProductImageUseCase.Queries;

public sealed class GETProductImagesQuery : IRequest<IEnumerable<ProductImageDto>>
{
    public int ProductId { get; set; }
}

public sealed class GETProductImagesQueryHandler : IRequestHandler<GETProductImagesQuery, IEnumerable<ProductImageDto>>
{
    private readonly IProductImageService _service;

    public GETProductImagesQueryHandler(IProductImageService service)
    {
        _service = service;
    }

    public async Task<IEnumerable<ProductImageDto>> Handle(GETProductImagesQuery query, CancellationToken ct)
    {
        return await _service.GetByProductAsync(query.ProductId, ct);
    }
}
