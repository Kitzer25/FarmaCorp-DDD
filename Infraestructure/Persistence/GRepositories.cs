using Core.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence;

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
        await  _dbSet.AddAsync(entity, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(int id, T entity, CancellationToken ct)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken ct)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(ct);
        return true;
    }
}