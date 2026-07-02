using Domain.DTO_s.Reports;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Report.Querys;

public sealed record GetDashboardSummaryQuery : IRequest<DashboardSummaryDto>;

public sealed class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, DashboardSummaryDto>
{
    private readonly IReportService _reportService;

    public GetDashboardSummaryQueryHandler(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<DashboardSummaryDto> Handle(GetDashboardSummaryQuery query, CancellationToken ct)
    {
        return await _reportService.GetDashboardSummaryAsync(ct);
    }
}
