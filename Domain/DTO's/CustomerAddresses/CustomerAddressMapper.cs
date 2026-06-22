using Core.Entities;

namespace Core.DTO_s.CustomerAddresses;

public static class CustomerAddressMapper
{
    public static CustomerAddressDto ToDto(CustomerAddress address)
    {
        return new CustomerAddressDto
        {
            AddressId = address.address_id,
            CustomerId = address.customer_id,
            Label = address.label,
            RecipientName = address.recipient_name,
            Street = address.street,
            District = address.district,
            City = address.city,
            State = address.state,
            PostalCode = address.postal_code,
            Country = address.country,
            Phone = address.phone,
            IsDefault = address.is_default
        };
    }
}
