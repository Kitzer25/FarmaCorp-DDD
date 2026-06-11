using Core.Entities;

namespace Core.Ports.Repositories.ERepository;

public interface IProductRepository :
    IGRepositories<Product>
{
    Task<Product?> GetBySlugAsync(string slug, CancellationToken ct);
    Task<IEnumerable<Product>> GetActiveAsync(CancellationToken ct);
}
