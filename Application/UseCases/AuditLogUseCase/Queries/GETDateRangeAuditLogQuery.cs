using Domain.DTO_s.AuditLog;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.AuditLogUseCase.Queries;

public sealed class GETDateRangeAuditLogQuery : IRequest<IEnumerable<AuditLogDto>>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}

public sealed class GETDateRangeAuditLogQueryHandler : IRequestHandler<GETDateRangeAuditLogQuery, IEnumerable<AuditLogDto>>
{
    private readonly IAuditService _auditService;

    public GETDateRangeAuditLogQueryHandler(IAuditService auditService)
    {
        _auditService = auditService;
    }

    public async Task<IEnumerable<AuditLogDto>> Handle(GETDateRangeAuditLogQuery query, CancellationToken ct)
    {
        return await _auditService.GetByDateRangeAsync(query.From, query.To, ct);
    }
}
