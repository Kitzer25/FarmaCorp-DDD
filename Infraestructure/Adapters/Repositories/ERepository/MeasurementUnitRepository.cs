using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class MeasurementUnitRepository : 
    GRepositories<MeasurementUnit>,
    IMeasurementUnitRepository
{ 
    public MeasurementUnitRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<MeasurementUnit?> GetBySymbolAsync(string symbol, CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(u => u.symbol == symbol, ct);

    public async Task<IEnumerable<MeasurementUnit>> GetAllActiveAsync(CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .Where(u => u.is_active)
            .OrderBy(u => u.name)
            .ToListAsync(ct);

    public async Task<bool> ExistsBySymbolAsync(string symbol, CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .AnyAsync(u => u.symbol == symbol, ct);

}
