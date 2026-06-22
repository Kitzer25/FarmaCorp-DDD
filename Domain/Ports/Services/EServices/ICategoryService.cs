using Core.DTO_s.Products;

namespace Core.Ports.Services.EServices;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}