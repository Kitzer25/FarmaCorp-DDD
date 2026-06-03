using Application.Products.Commands;
using Application.Products.Dtos;
using Application.Products.Services;
using MediatR;

namespace Application.Products.Handlers;

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