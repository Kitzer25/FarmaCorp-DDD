using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ICustomerAddressRepository : 
    IGRepositories<CustomerAddress>
{
    Task<IEnumerable<CustomerAddress>> GetActiveByCustomerIdAsync(int customerId, CancellationToken ct);
    Task<CustomerAddress?> GetActiveByIdAndCustomerIdAsync(int addressId, int customerId, CancellationToken ct);
    Task ClearDefaultForCustomerAsync(int customerId, int? exceptAddressId, CancellationToken ct);
}
