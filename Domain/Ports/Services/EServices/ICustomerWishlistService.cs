using Domain.DTO_s.CustomerWhislist;

namespace Domain.Ports.Services.EServices;

public interface ICustomerWishlistService
{
    Task<IEnumerable<CustomerWishlistDto>> GetByCustomerAsync(int customerId, CancellationToken ct);
    Task<CustomerWishlistDto> AddAsync(int customerId, int productVariantId, CancellationToken ct);
    Task<bool> RemoveAsync(int customerId, int productVariantId, CancellationToken ct);
    Task<IEnumerable<MostWishlistedVariantDto>> GetMostWishlistedAsync(int top, CancellationToken ct);
}
