using Application.Categories.Commands;
using Application.Categories.Dtos;
using Application.Categories.Services;
using MediatR;

namespace Application.Categories.Handlers;

public class GetCategoriesCommandHandler : IRequestHandler<GetCategoriesCommand, List<CategoryDto>>
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