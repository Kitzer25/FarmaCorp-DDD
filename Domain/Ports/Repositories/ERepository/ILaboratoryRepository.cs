using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ILaboratoryRepository : 
    IGRepositories<Laboratory>
{
    Task<Laboratory?> GetByNameAsync(string name, CancellationToken ct);

    Task<IEnumerable<Laboratory>> GetAllActiveAsync(CancellationToken ct);

    Task<IEnumerable<Laboratory>> GetByCountryAsync(string country, CancellationToken ct);

    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);

}