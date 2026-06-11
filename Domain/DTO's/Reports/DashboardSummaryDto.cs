namespace Application.Reports.Dtos;

public class DashboardSummaryDto
{
    public int TotalOrders { get; set; }
    public decimal TotalSales { get; set; }
    public int TotalRevenueOrders { get; set; }
    public int LowStockProducts { get; set; }
    public List<TopProductDto> TopProducts { get; set; } = new();
}
