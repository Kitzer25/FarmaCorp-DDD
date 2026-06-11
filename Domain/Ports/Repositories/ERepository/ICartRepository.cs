using Core.Entities;

namespace Core.Ports.Repositories.ERepository;

public interface ICartRepository : 
    IGRepositories<Cart>
{
    Task<Cart?> GetActiveByCustomerIdWithItemsAsync(int customerId, CancellationToken ct);
}
