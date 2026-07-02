using Domain.Ports.Repositories;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories;

public class GRepositories<T> : IGRepositories<T>
    where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GRepositories(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct)
    {
        return await _dbSet.AsNoTracking().ToListAsync(ct);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct)
    {
        return await _dbSet.FindAsync(id, ct);
    }

    public async Task AddAsync(T entity, CancellationToken ct)
    {
        await _dbSet.AddAsync(entity, ct);
    }

    public Task UpdateAsync(int id, T entity, CancellationToken ct)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(T entity, CancellationToken ct)
    {
        _dbSet.Remove(entity);
        return Task.FromResult(true);
    }
}
