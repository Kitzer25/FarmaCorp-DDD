using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IDiscountTypeRepository : 
    IGRepositories<DiscountType>
{
    Task<DiscountType?> GetByNameAsync(string name, CancellationToken ct);

    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);

}