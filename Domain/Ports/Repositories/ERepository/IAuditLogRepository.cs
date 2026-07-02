using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IAuditLogRepository : IGRepositories<AuditLog>
{
    Task<IEnumerable<AuditLog>> GetByRecordAsync(string tableName, string recordId, CancellationToken ct);

    Task<IEnumerable<AuditLog>> GetByUserAsync(int userId, DateTime from, DateTime to, CancellationToken ct);

    Task<IEnumerable<AuditLog>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct);
}
