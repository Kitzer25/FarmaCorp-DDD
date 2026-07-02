using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ICustomerRepository : 
    IGRepositories<Customer>
{
    Task<Customer?> GetByEmailAsync(string email, CancellationToken ct);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken ct);
}