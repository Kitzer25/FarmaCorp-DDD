using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class OrderStatusHistoryRepository : 
    GRepositories<OrderStatusHistory>,
    IOrderStatusHistoryRepository
{ 
    public OrderStatusHistoryRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<IEnumerable<OrderStatusHistory>> GetByOrderAsync(int orderId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .Where(h => h.order_id == orderId)
            .OrderBy(h => h.created_at)
            .ToListAsync(ct);
}
