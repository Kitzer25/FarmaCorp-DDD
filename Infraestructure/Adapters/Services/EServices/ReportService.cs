using Domain.Ports;
using Domain.Ports.Services;
using Domain.DTO_s.Reports;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

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
        var orderSummaries = await _unitOfWork.VCustomerOrderSumaryRepo.GetAllAsync(ct);
        var availableStock = await _unitOfWork.VAvalibleStockRepo.GetAllAsync(ct);

        return new DashboardSummaryDto
        {
            TotalOrders = (int)orderSummaries.Sum(s => s.total_orders ?? 0),
            TotalRevenueOrders = orders.Count(o => o.total > 0),
            TotalSales = orderSummaries.Sum(s => s.total_spent ?? 0),
            LowStockProducts = availableStock.Count(s => s.is_low_stock == true),
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
