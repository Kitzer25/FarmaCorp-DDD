using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface ICustomerWishlistRepository : 
    IGRepositories<CustomerWishlist>
{
    Task<IEnumerable<CustomerWishlist>> GetByCustomerAsync(int customerId, CancellationToken ct);

    Task<CustomerWishlist?> GetByKeyAsync(int customerId, int productVariantId, CancellationToken ct);

    Task<bool> ExistsAsync(int customerId, int productVariantId, CancellationToken ct);

    Task<IEnumerable<(int ProductVariantId, int Total)>> GetMostWishlistedAsync(int top, CancellationToken ct);

}