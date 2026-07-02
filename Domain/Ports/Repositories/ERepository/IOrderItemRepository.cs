using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IOrderItemRepository : 
    IGRepositories<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetByOrderAsync(int orderId, CancellationToken ct);

    Task<IEnumerable<OrderItem>> GetByVariantAsync(int productVariantId, CancellationToken ct);

    Task<IEnumerable<(int ProductVariantId, int TotalQuantity, decimal TotalRevenue)>> GetTopSellingAsync(int top, CancellationToken ct);

}