using Domain.DTO_s.AuditLog;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.AuditLogUseCase.Queries;

public sealed class GETRecordAuditLogQuery : IRequest<IEnumerable<AuditLogDto>>
{
    public string TableName { get; set; } = null!;
    public string RecordId { get; set; } = null!;
}

public sealed class GETRecordAuditLogQueryHandler : IRequestHandler<GETRecordAuditLogQuery, IEnumerable<AuditLogDto>>
{
    private readonly IAuditService _auditService;

    public GETRecordAuditLogQueryHandler(IAuditService auditService)
    {
        _auditService = auditService;
    }

    public async Task<IEnumerable<AuditLogDto>> Handle(GETRecordAuditLogQuery query, CancellationToken ct)
    {
        return await _auditService.GetByRecordAsync(query.TableName, query.RecordId, ct);
    }
}
