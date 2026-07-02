using Domain.DTO_s.Products;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.ProductUseCase.Queries;

public class GETProductDetailQuery : IRequest<ProductDetailDto>
{
    public string Slug { get; set; } = null!;
}

public sealed class GETProductDetailQueryHandler : IRequestHandler<GETProductDetailQuery, ProductDetailDto>
{
    private readonly IProductService _productService;

    public GETProductDetailQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDetailDto> Handle(GETProductDetailQuery request, CancellationToken ct)
    {
        return await _productService.GetProductDetailAsync(request.Slug, ct);
    }
}
