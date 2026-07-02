using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ICartRepository : 
    IGRepositories<Cart>
{
    Task<Cart?> GetActiveByCustomerIdWithItemsAsync(int customerId, CancellationToken ct);
}
