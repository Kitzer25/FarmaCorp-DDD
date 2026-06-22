using Core.DTO_s.Reports;

namespace Core.Ports.Services.EServices;

public interface IReportService
{
    Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken ct);
}