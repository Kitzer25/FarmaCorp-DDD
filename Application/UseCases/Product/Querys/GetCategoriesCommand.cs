using Application.Categories.Dtos;
using Core.Ports.Services;
using MediatR;

namespace Application.UseCases.Product.Querys;

public class GetCategoriesCommand : IRequest<List<CategoryDto>>
{
}

public sealed class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, List<CategoryDto>>
{
    private readonly ICategoryService _categoryService;

    public GetCategoriesCommandHandler(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetCategoriesAsync(cancellationToken);
    }
}