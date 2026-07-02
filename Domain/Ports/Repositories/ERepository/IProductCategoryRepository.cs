using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IProductCategoryRepository : 
    IGRepositories<ProductCategory>
{
    Task<IEnumerable<ProductCategory>> GetByProductAsync(int productId, CancellationToken ct);

    Task<IEnumerable<ProductCategory>> GetByCategoryAsync(int categoryId, CancellationToken ct);

    Task<ProductCategory?> GetByKeyAsync(int productId, int categoryId, CancellationToken ct);

    Task<ProductCategory?> GetPrimaryByProductAsync(int productId, CancellationToken ct);

    Task<bool> ExistsAsync(int productId, int categoryId, CancellationToken ct);

}