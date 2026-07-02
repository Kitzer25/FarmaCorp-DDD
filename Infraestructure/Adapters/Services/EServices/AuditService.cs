using System.Text.Json;
using Domain.DTO_s.AuditLog;
using Domain.DTO_s.AuditLog.Mappers;
using Domain.Ports;
using Domain.Ports.Services;
using Domain.Entities;
using Domain.Ports.Repositories;
using Domain.Ports.Services.EServices;

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
        var log = AuditLogMapper.ToEntity(
            tableName,
            recordId,
            action,
            oldValues == null ? null : JsonSerializer.Serialize(oldValues),
            newValues == null ? null : JsonSerializer.Serialize(newValues),
            userId,
            customerId);

        await _unitOfWork.AuditLogRepo.AddAsync(log, ct);
    }

    public async Task<IEnumerable<AuditLogDto>> GetByRecordAsync(string tableName, string recordId, CancellationToken ct)
    {
        var logs = await _unitOfWork.AuditLogRepo.GetByRecordAsync(tableName, recordId, ct);
        return logs.Select(l => l.ToDto());
    }

    public async Task<IEnumerable<AuditLogDto>> GetByUserAsync(int userId, DateTime from, DateTime to, CancellationToken ct)
    {
        var logs = await _unitOfWork.AuditLogRepo.GetByUserAsync(userId, from, to, ct);
        return logs.Select(l => l.ToDto());
    }

    public async Task<IEnumerable<AuditLogDto>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct)
    {
        var logs = await _unitOfWork.AuditLogRepo.GetByDateRangeAsync(from, to, ct);
        return logs.Select(l => l.ToDto());
    }
}
