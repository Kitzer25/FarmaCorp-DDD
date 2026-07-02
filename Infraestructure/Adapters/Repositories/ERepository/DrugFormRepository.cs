using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class DrugFormRepository : 
    GRepositories<DrugForm>,
    IDrugFormRepository
{ 
    public DrugFormRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<DrugForm?> GetByNameAsync(string name, CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(d => d.name == name, ct);

    public async Task<IEnumerable<DrugForm>> GetAllActiveAsync(CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .Where(d => d.is_active)
            .OrderBy(d => d.name)
            .ToListAsync(ct);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct) =>

        await _dbSet.AsNoTracking()
            .AnyAsync(d => d.name == name, ct);

}
