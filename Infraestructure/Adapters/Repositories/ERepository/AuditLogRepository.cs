using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class AuditLogRepository : GRepositories<AuditLog>,
    IAuditLogRepository
{
    public AuditLogRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<AuditLog>> GetByRecordAsync(string tableName, string recordId, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(a => a.table_name == tableName && a.record_id == recordId)

            .OrderByDescending(a => a.created_at)

            .ToListAsync(ct);

    public async Task<IEnumerable<AuditLog>> GetByUserAsync(int userId, DateTime from, DateTime to, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(a => a.user_id == userId && a.created_at >= from && a.created_at <= to)

            .OrderByDescending(a => a.created_at)

            .ToListAsync(ct);

    public async Task<IEnumerable<AuditLog>> GetByDateRangeAsync(DateTime from, DateTime to, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(a => a.created_at >= from && a.created_at <= to)

            .OrderByDescending(a => a.created_at)

            .ToListAsync(ct);

}