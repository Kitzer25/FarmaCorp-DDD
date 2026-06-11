using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class OrderRepository : 
    GRepositories<Order>,
    IOrderRepository
{ 
    public OrderRepository(AppDbContext context) : base(context)
    { }

    public async Task<Order?> GetByIdWithDetailsAsync(int orderId, CancellationToken ct)
    {
        return await _context.orders
            .Include(o => o.order_items)
            .Include(o => o.order_status)
            .Include(o => o.shipping_address)
            .Include(o => o.order_payments)
            .Include(o => o.prescription_uploads)
            .FirstOrDefaultAsync(o => o.order_id == orderId, ct);
    }

    public async Task<string?> GetLastOrderNumberForYearAsync(int year, CancellationToken ct)
    {
        var prefix = $"ORD-{year}-";

        return await _context.orders
            .AsNoTracking()
            .Where(o => o.order_number.StartsWith(prefix))
            .OrderByDescending(o => o.order_number)
            .Select(o => o.order_number)
            .FirstOrDefaultAsync(ct);
    }
}
