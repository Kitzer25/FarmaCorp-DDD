using Domain.DTO_s.AuditLog;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.AuditLogUseCase.Queries;

public sealed class GETUserAuditLogQuery : IRequest<IEnumerable<AuditLogDto>>
{
    public int UserId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}

public sealed class GETUserAuditLogQueryHandler : IRequestHandler<GETUserAuditLogQuery, IEnumerable<AuditLogDto>>
{
    private readonly IAuditService _auditService;

    public GETUserAuditLogQueryHandler(IAuditService auditService)
    {
        _auditService = auditService;
    }

    public async Task<IEnumerable<AuditLogDto>> Handle(GETUserAuditLogQuery query, CancellationToken ct)
    {
        return await _auditService.GetByUserAsync(query.UserId, query.From, query.To, ct);
    }
}
