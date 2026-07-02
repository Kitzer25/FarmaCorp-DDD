using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IOrderPaymentRepository : 
    IGRepositories<OrderPayment>
{
    Task<IEnumerable<OrderPayment>> GetByOrderAsync(int orderId, CancellationToken ct);

    Task<OrderPayment?> GetByTransactionReferenceAsync(string reference, CancellationToken ct);

    Task<decimal> GetTotalPaidByOrderAsync(int orderId, CancellationToken ct);

}