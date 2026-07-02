using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IProductRepository :
    IGRepositories<Product>
{
    Task<Product?> GetBySlugAsync(string slug, CancellationToken ct);
    Task<IEnumerable<Product>> GetActiveAsync(CancellationToken ct);
}
