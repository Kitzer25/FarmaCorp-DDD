using Application.Products.Commands;
using Application.Products.Dtos;
using Application.Products.Services;
using MediatR;

namespace Application.Products.Handlers;

public class GetProductDetailCommandHandler : IRequestHandler<GetProductDetailCommand, ProductDetailDto>
{
    private readonly IProductService _productService;

    public GetProductDetailCommandHandler(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<ProductDetailDto> Handle(GetProductDetailCommand request, CancellationToken cancellationToken)
    {
        return await _productService.GetProductDetailAsync(request.Slug, cancellationToken);
    }
}