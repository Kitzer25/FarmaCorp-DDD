using CategoryDto = Domain.DTO_s.Products.CategoryDto;

namespace Domain.Ports.Services.EServices;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}