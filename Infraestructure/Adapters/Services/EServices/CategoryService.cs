using Domain.DTO_s.Categories;
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

    public Task<IEnumerable<Category>> GetAllAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    Task<Category?> IGRepositories<Category>.GetByIdAsync(int id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Category entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, Category entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Category entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<CategoryDto?> GetByIdAsync(int categoryId, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<CategoryDto> UpdateAsync(int categoryId, UpdateCategoryDto dto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int categoryId, CancellationToken ct)
    {
        var category = await _unitOfWork.CategoryRepo.GetByIdAsync(categoryId, ct);

        if (category == null)
        {
            return false;
        }

        await _unitOfWork.CategoryRepo.DeleteAsync(category, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }
}