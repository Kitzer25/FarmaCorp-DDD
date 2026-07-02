using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IOrderStatusHistoryRepository :
    IGRepositories<OrderStatusHistory>
{
    Task<IEnumerable<OrderStatusHistory>> GetByOrderAsync(int orderId, CancellationToken ct);
}