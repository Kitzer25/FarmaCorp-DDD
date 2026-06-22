using System.Text.Json;
using Core.Entities;
using Core.Ports;
using Core.Ports.Repositories;
using Core.Ports.Services;
using Core.Ports.Services.EServices;

namespace Infraestructure.Adapters.Services.EServices;

public class AuditService : IAuditService
{
    private readonly IUnitOfWork _unitOfWork;

    public AuditService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task RegisterAsync(
        string tableName,
        string recordId,
        string action,
        object? oldValues,
        object? newValues,
        int? userId,
        int? customerId,
        CancellationToken ct)
    {
        await _unitOfWork.Repositories<AuditLog>().AddAsync(new AuditLog
        {
            table_name = tableName,
            record_id = recordId,
            action = action,
            old_values = oldValues == null ? null : JsonSerializer.Serialize(oldValues),
            new_values = newValues == null ? null : JsonSerializer.Serialize(newValues),
            user_id = userId,
            customer_id = customerId,
            created_at = DateTime.UtcNow
        }, ct);
    }
}
