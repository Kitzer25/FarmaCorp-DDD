using Core.Entities;

namespace Core.Ports.Repositories.ERepository;

public interface ICategoryRepository : IGRepositories<Category>
{
    Task<IEnumerable<Category>> GetActiveAsync(CancellationToken ct);
}