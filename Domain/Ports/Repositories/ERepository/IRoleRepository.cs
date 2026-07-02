using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IRoleRepository :
    IGRepositories<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken ct);

    Task<IEnumerable<Role>> GetAllActiveAsync(CancellationToken ct);

    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);

}