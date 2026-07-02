using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IDrugFormRepository : 
    IGRepositories<DrugForm>
{
    Task<DrugForm?> GetByNameAsync(string name, CancellationToken ct);

    Task<IEnumerable<DrugForm>> GetAllActiveAsync(CancellationToken ct);

    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);

}