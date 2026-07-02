using Domain.DTO_s.CustomerAddresses;
using Domain.DTO_s.CustomerAddresses.Mapper;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class CustomerAddressService : ICustomerAddressService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerAddressService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CustomerAddressDto>> GetActiveByCustomerAsync(int customerId, CancellationToken ct)
    {
        var addresses = await _unitOfWork.CustomerAddressRepo.GetActiveByCustomerIdAsync(customerId, ct);

        return addresses.Select(CustomerAddressMapper.ToDto);
    }

    public async Task<CustomerAddressDto> CreateAsync(int customerId, SaveCustomerAddressDto dto, CancellationToken ct)
    {
        if (dto.IsDefault)
        {
            await _unitOfWork.CustomerAddressRepo.ClearDefaultForCustomerAsync(customerId, null, ct);
        }

        var address = new Domain.Entities.CustomerAddress
        {
            customer_id = customerId,
            label = dto.Label,
            recipient_name = dto.RecipientName,
            street = dto.Street.Trim(),
            district = dto.District,
            city = dto.City.Trim(),
            state = dto.State,
            postal_code = dto.PostalCode,
            country = string.IsNullOrWhiteSpace(dto.Country) ? "Peru" : dto.Country.Trim(),
            phone = dto.Phone,
            is_default = dto.IsDefault,
            is_active = true,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.CustomerAddressRepo.AddAsync(address, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return CustomerAddressMapper.ToDto(address);
    }

    public async Task<CustomerAddressDto> UpdateAsync(int customerId, int addressId, SaveCustomerAddressDto dto, CancellationToken ct)
    {
        var address = await GetOwnedActiveAddressAsync(customerId, addressId, ct);

        if (dto.IsDefault)
        {
            await _unitOfWork.CustomerAddressRepo.ClearDefaultForCustomerAsync(customerId, addressId, ct);
        }

        address.label = dto.Label;
        address.recipient_name = dto.RecipientName;
        address.street = dto.Street.Trim();
        address.district = dto.District;
        address.city = dto.City.Trim();
        address.state = dto.State;
        address.postal_code = dto.PostalCode;
        address.country = string.IsNullOrWhiteSpace(dto.Country) ? "Peru" : dto.Country.Trim();
        address.phone = dto.Phone;
        address.is_default = dto.IsDefault;
        address.updated_at = DateTime.UtcNow;

        await _unitOfWork.CustomerAddressRepo.UpdateAsync(address.address_id, address, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return CustomerAddressMapper.ToDto(address);
    }

    public async Task<CustomerAddressDto> SetDefaultAsync(int customerId, int addressId, CancellationToken ct)
    {
        var address = await GetOwnedActiveAddressAsync(customerId, addressId, ct);

        await _unitOfWork.CustomerAddressRepo.ClearDefaultForCustomerAsync(customerId, addressId, ct);

        address.is_default = true;
        address.updated_at = DateTime.UtcNow;

        await _unitOfWork.CustomerAddressRepo.UpdateAsync(address.address_id, address, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return CustomerAddressMapper.ToDto(address);
    }

    public async Task DeleteAsync(int customerId, int addressId, CancellationToken ct)
    {
        var address = await GetOwnedActiveAddressAsync(customerId, addressId, ct);

        address.is_active = false;
        address.is_default = false;
        address.updated_at = DateTime.UtcNow;

        await _unitOfWork.CustomerAddressRepo.UpdateAsync(address.address_id, address, ct);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    private async Task<Domain.Entities.CustomerAddress> GetOwnedActiveAddressAsync(int customerId, int addressId, CancellationToken ct)
    {
        var address = await _unitOfWork.CustomerAddressRepo
            .GetActiveByIdAndCustomerIdAsync(addressId, customerId, ct);

        if (address == null)
        {
            throw new KeyNotFoundException("La dirección no existe o no pertenece al cliente.");
        }

        return address;
    }
}
