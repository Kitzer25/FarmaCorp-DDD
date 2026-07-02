using Domain.DTO_s.Products;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.ProductUseCase.Queries;

public class GETProductsQuery : IRequest<List<ProductListDto>>
{
    public ProductQueryParams Params { get; set; } = new();
}

public class GETProductsQueryHandler : IRequestHandler<GETProductsQuery, List<ProductListDto>>
{
    private readonly IProductService _productService;

    public GETProductsQueryHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductListDto>> Handle(GETProductsQuery request, CancellationToken ct)
    {
        return await _productService.GetProductsAsync(request.Params, ct);
    }
}
