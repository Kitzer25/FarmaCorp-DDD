using Application.Reports.Dtos;
using Core.Ports;
using Core.Ports.Services;

namespace Infraestructure.Adapters.Services;

public class ReportService : IReportService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken ct)
    {
        var orders = (await _unitOfWork.OrderRepo.GetAllAsync(ct)).ToList();
        var orderItems = (await _unitOfWork.OrderItemRepo.GetAllAsync(ct)).ToList();
        var inventory = await _unitOfWork.InventoryRepo.GetAllWithProductAsync(ct);

        return new DashboardSummaryDto
        {
            TotalOrders = orders.Count,
            TotalRevenueOrders = orders.Count(o => o.total > 0),
            TotalSales = orders.Sum(o => o.total),
            LowStockProducts = inventory.Count(i => Math.Max(0, i.quantity_on_hand - i.reserved_quantity) <= i.min_stock_level),
            TopProducts = orderItems
                .GroupBy(i => new { i.product_variant_id, i.product_name_snapshot, i.sku_snapshot })
                .OrderByDescending(g => g.Sum(i => i.quantity))
                .Take(5)
                .Select(g => new TopProductDto
                {
                    ProductVariantId = g.Key.product_variant_id,
                    ProductName = g.Key.product_name_snapshot,
                    Sku = g.Key.sku_snapshot,
                    QuantitySold = g.Sum(i => i.quantity),
                    TotalSold = g.Sum(i => i.subtotal)
                })
                .ToList()
        };
    }
}
