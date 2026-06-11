using Application.Reports.Dtos;

namespace Core.Ports.Services;

public interface IReportService
{
    Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken ct);
}