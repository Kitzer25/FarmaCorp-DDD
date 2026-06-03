using Application.Categories.Dtos;

namespace Application.Categories.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}