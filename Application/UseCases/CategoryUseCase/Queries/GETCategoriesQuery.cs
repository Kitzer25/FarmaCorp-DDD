using Domain.DTO_s.Products;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CategoryUseCase.Queries;

public sealed record GETCategoriesQuery : IRequest<List<CategoryDto>>;

public sealed class GETCategoriesQueryHandler : IRequestHandler<GETCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryService _service;

    public GETCategoriesQueryHandler(ICategoryService service)
    {
        _service = service;
    }

    public async Task<List<CategoryDto>> Handle(GETCategoriesQuery query, CancellationToken ct)
    {
        return await _service.GetCategoriesAsync(ct);
    }
}
