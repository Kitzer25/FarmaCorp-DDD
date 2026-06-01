namespace Application.Audit.Services;

public interface IAuditService
{
    Task RegisterAsync(string tableName, string recordId, string action, object? oldValues, object? newValues, int? userId, int? customerId, CancellationToken ct);
}
