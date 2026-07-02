using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IOrderRepository :
    IGRepositories<Order>
{
    Task<Order?> GetByIdWithDetailsAsync(int orderId, CancellationToken ct);
    Task<string?> GetLastOrderNumberForYearAsync(int year, CancellationToken ct);
}
