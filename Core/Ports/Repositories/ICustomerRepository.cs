using Core.Entities;

namespace Core.Ports.Repositories;

public interface ICustomerRepository : 
    IGRepositories<Customer>
{
    Task<Customer?> GetByEmailAsync(string email, CancellationToken ct);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct);
}