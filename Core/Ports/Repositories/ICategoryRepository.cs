using Core.Entities;

namespace Core.Ports.Repositories;

public interface ICategoryRepository : IGRepositories<Category>
{
    Task<IEnumerable<Category>> GetActiveAsync(CancellationToken ct);
}