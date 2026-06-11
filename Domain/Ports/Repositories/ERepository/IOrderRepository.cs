using Core.Entities;

namespace Core.Ports.Repositories.ERepository;

public interface IOrderRepository :
    IGRepositories<Order>
{
    Task<Order?> GetByIdWithDetailsAsync(int orderId, CancellationToken ct);
    Task<string?> GetLastOrderNumberForYearAsync(int year, CancellationToken ct);
}
