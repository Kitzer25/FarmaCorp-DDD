using Application.Categories.Dtos;
using Core.Ports;

namespace Application.Categories.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct)
    {
        var categories = await _unitOfWork.CategoryRepo.GetActiveAsync(ct);

        // Solo categorías raíz, las subcategorías van anidadas dentro
        return categories
            .Where(c => c.parent_category_id == null)
            .OrderBy(c => c.sort_order)
            .Select(c => new CategoryDto
            {
                CategoryId       = c.category_id,
                Name             = c.name,
                Slug             = c.slug,
                Description      = c.description,
                ParentCategoryId = c.parent_category_id,
                SortOrder        = c.sort_order,
                SubCategories    = categories
                    .Where(s => s.parent_category_id == c.category_id)
                    .OrderBy(s => s.sort_order)
                    .Select(s => new CategoryDto
                    {
                        CategoryId       = s.category_id,
                        Name             = s.name,
                        Slug             = s.slug,
                        Description      = s.description,
                        ParentCategoryId = s.parent_category_id,
                        SortOrder        = s.sort_order
                    }).ToList()
            }).ToList();
    }
}