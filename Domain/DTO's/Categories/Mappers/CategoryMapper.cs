using Domain.Entities;
using ProductCategoryDto = Domain.DTO_s.Products.CategoryDto;

namespace Domain.DTO_s.Categories.Mappers;

public static class CategoryMapper
{
    /// <summary>
    /// Mapeo plano, sin subcategorías.
    /// </summary>
    public static ProductCategoryDto ToDto(this Category category)
    {
        return new ProductCategoryDto
        {
            CategoryId = category.category_id,
            Name = category.name,
            Slug = category.slug,
            Description = category.description,
            ParentCategoryId = category.parent_category_id,
            SortOrder = category.sort_order
        };
    }

    /// <summary>
    /// Construye el árbol de un nivel (raíz + subcategorías directas) a partir de una lista plana.
    /// </summary>
    public static List<ProductCategoryDto> ToDtoTree(this IEnumerable<Category> categories)
    {
        var categoryList = categories.ToList();

        return categoryList
            .Where(c => c.parent_category_id == null)
            .OrderBy(c => c.sort_order)
            .Select(c => c.ToDtoWithChildren(categoryList))
            .ToList();
    }

    private static ProductCategoryDto ToDtoWithChildren(this Category category, IEnumerable<Category> allCategories)
    {
        var dto = category.ToDto();

        dto.SubCategories = allCategories
            .Where(s => s.parent_category_id == category.category_id)
            .OrderBy(s => s.sort_order)
            .Select(s => s.ToDto())
            .ToList();

        return dto;
    }
}
