using Domain.DTO_s.AuditLog;

namespace Domain.Ports.Services.EServices;

public interface IAuditService
{
    Task RegisterAsync(string tableName, string recordId, string action, object? oldValues, object? newValues,
        int? userId, int? customerId, CancellationToken ct);
    
    Task<IEnumerable<AuditLogDto>> GetByRecordAsync(string tableName, string recordId, CancellationToken ct);

    Task<IEnumerable<AuditLogDto>> GetByUserAsync(int userId, DateTime from, DateTime to, CancellationToken ct);

    Task<IEnumerable<AuditLogDto>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct);

}