using Application.Categories.Dtos;

namespace Core.Ports.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}