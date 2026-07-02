using Domain.DTO_s.Reports;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.ReportUseCase.Queries;

public sealed record GETDashboardSummaryQuery : IRequest<DashboardSummaryDto>;

public sealed class GETDashboardSummaryQueryHandler : IRequestHandler<GETDashboardSummaryQuery, DashboardSummaryDto>
{
    private readonly IReportService _reportService;

    public GETDashboardSummaryQueryHandler(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<DashboardSummaryDto> Handle(GETDashboardSummaryQuery query, CancellationToken ct)
    {
        return await _reportService.GetDashboardSummaryAsync(ct);
    }
}
