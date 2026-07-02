using Domain.DTO_s.Categories.Mappers;
using Domain.Entities;
using Domain.Ports;
using Domain.Ports.Services;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;
using CategoryDto = Domain.DTO_s.Products.CategoryDto;

namespace Infraestructure.Adapters.Services.EServices;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct)
    {
        IEnumerable<Category> categories = await _unitOfWork.CategoryRepo.GetActiveAsync(ct);
        return CategoryMapper.ToDtoTree(categories);
    }
}