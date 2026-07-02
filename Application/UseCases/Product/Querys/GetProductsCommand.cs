using Domain.Ports.Services;
using Domain.DTO_s.Products;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Product.Querys;

public class GetProductsQuery : IRequest<List<ProductListDto>>
{
    public ProductQueryParams Params { get; set; } = new();
}

public class GetProductsCommandHandler : IRequestHandler<GetProductsQuery, List<ProductListDto>>
{
    private readonly IProductService _productService;

    public GetProductsCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductListDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productService.GetProductsAsync(request.Params, cancellationToken);
    }
}