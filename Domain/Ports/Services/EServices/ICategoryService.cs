using Domain.DTO_s.Categories;
using Domain.Entities;
using Domain.Ports.Repositories;
using CategoryDto = Domain.DTO_s.Products.CategoryDto;

namespace Domain.Ports.Services.EServices;

public interface ICategoryService : IGRepositories<Category>
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}