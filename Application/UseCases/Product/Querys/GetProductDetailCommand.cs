using Core.DTO_s.Products;
using Core.Ports.Services;
using Core.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Product.Querys;

public class GetProductDetailCommand : IRequest<ProductDetailDto>
{
    public string Slug { get; set; } = null!;
}

public sealed class GetProductDetailCommandHandler : IRequestHandler<GetProductDetailCommand, ProductDetailDto>
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