using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class LaboratoryRepository : 
    GRepositories<Laboratory>,
    ILaboratoryRepository
{ 
    public LaboratoryRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<Laboratory?> GetByNameAsync(string name, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .FirstOrDefaultAsync(l => l.name == name, ct);

    public async Task<IEnumerable<Laboratory>> GetAllActiveAsync(CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(l => l.is_active)

            .OrderBy(l => l.name)

            .ToListAsync(ct);

    public async Task<IEnumerable<Laboratory>> GetByCountryAsync(string country, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .Where(l => l.country_of_origin == country)

            .OrderBy(l => l.name)

            .ToListAsync(ct);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct) =>

        await _dbSet.AsNoTracking()

            .AnyAsync(l => l.name == name, ct);

}
