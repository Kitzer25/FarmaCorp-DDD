using Domain.DTO_s.Reports;

namespace Domain.Ports.Services.EServices;

public interface IReportService
{
    Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken ct);
}