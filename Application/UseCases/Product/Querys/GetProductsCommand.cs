using Application.Products.Dtos;
using Core.Ports.Services;
using MediatR;

namespace Application.UseCases.Product.Querys;

public class GetProductsCommand : IRequest<List<ProductListDto>>
{
    public ProductQueryParams Params { get; set; } = new();
}

public class GetProductsCommandHandler : IRequestHandler<GetProductsCommand, List<ProductListDto>>
{
    private readonly IProductService _productService;

    public GetProductsCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<List<ProductListDto>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        return await _productService.GetProductsAsync(request.Params, cancellationToken);
    }
}