using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class OrderPaymentRepository : 
    GRepositories<OrderPayment>,
    IOrderPaymentRepository
{ 
    public OrderPaymentRepository(AppDbContext context) : base(context)
    { }  
    
    private const string PaidStatus = "Paid";

    public async Task<IEnumerable<OrderPayment>> GetByOrderAsync(int orderId, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(p => p.order_id == orderId)
            .OrderByDescending(p => p.created_at)
            .ToListAsync(ct);

    public async Task<OrderPayment?> GetByTransactionReferenceAsync(string reference, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(p => p.transaction_reference == reference, ct);

    // Insight: suma de pagos confirmados de un pedido (para saber si está saldado).

    public async Task<decimal> GetTotalPaidByOrderAsync(int orderId, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(p => p.order_id == orderId && p.payment_status == PaidStatus)
            .SumAsync(p => p.amount, ct);
}
