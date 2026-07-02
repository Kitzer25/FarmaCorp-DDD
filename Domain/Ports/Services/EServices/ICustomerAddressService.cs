using Domain.DTO_s.CustomerAddresses;

namespace Domain.Ports.Services.EServices;

public interface ICustomerAddressService
{
    Task<IEnumerable<CustomerAddressDto>> GetActiveByCustomerAsync(int customerId, CancellationToken ct);
    Task<CustomerAddressDto> CreateAsync(int customerId, SaveCustomerAddressDto dto, CancellationToken ct);
    Task<CustomerAddressDto> UpdateAsync(int customerId, int addressId, SaveCustomerAddressDto dto, CancellationToken ct);
    Task<CustomerAddressDto> SetDefaultAsync(int customerId, int addressId, CancellationToken ct);
    Task DeleteAsync(int customerId, int addressId, CancellationToken ct);
}
