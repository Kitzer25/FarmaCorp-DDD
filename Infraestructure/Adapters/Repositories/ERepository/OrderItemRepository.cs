using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class OrderItemRepository : 
    GRepositories<OrderItem>,
    IOrderItemRepository
{ 
    public OrderItemRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<IEnumerable<OrderItem>> GetByOrderAsync(int orderId, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(i => i.order_id == orderId)
            .ToListAsync(ct);

    public async Task<IEnumerable<OrderItem>> GetByVariantAsync(int productVariantId, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(i => i.product_variant_id == productVariantId)
            .ToListAsync(ct);

    // Insight admin: ranking de variantes por unidades vendidas e ingresos.

    public async Task<IEnumerable<(int ProductVariantId, int TotalQuantity, decimal TotalRevenue)>> GetTopSellingAsync(int top, CancellationToken ct)
    {

        var result = await _dbSet.AsNoTracking()
            .GroupBy(i => i.product_variant_id)
            .Select(g => new
            {
                ProductVariantId = g.Key,
                TotalQuantity = g.Sum(x => x.quantity),
                TotalRevenue = g.Sum(x => x.subtotal)
            })
            .OrderByDescending(x => x.TotalQuantity)
            .Take(top)
            .ToListAsync(ct);
        return result.Select(x => (x.ProductVariantId, x.TotalQuantity, x.TotalRevenue));

    }

}
