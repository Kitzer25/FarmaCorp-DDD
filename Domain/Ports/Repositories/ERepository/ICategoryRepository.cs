using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ICategoryRepository : IGRepositories<Category>
{
    Task<IEnumerable<Category>> GetActiveAsync(CancellationToken ct);
}