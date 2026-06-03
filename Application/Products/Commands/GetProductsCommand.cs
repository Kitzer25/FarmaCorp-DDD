using Application.Products.Dtos;
using MediatR;

namespace Application.Products.Commands;

public class GetProductsCommand : IRequest<List<ProductListDto>>
{
    public ProductQueryParams Params { get; set; } = new();
}