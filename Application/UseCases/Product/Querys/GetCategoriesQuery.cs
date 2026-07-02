using Domain.Ports.Services;
using Domain.DTO_s.Products;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Product.Querys;

public sealed record GetCategoriesQuery : IRequest<List<CategoryDto>>;

public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryService _service;

    public GetCategoriesQueryHandler(ICategoryService service)
    {
        _service = service;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery query, CancellationToken ct)
    {
        return await _service.GetCategoriesAsync(ct);
    }
}