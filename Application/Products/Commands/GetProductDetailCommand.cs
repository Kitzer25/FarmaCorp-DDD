using Application.Products.Dtos;
using MediatR;

namespace Application.Products.Commands;

public class GetProductDetailCommand : IRequest<ProductDetailDto>
{
    public string Slug { get; set; } = null!;
}