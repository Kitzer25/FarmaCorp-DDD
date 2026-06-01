using Application.Reports.Dtos;

namespace Application.Reports.Services;

public interface IReportService
{
    Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken ct);
}
