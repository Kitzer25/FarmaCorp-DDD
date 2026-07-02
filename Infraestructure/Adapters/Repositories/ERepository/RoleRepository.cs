using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class RoleRepository : 
    GRepositories<Role>,
    IRoleRepository
{ 
    public RoleRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<Role?> GetByNameAsync(string name, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(r => r.name == name, ct);

    public async Task<IEnumerable<Role>> GetAllActiveAsync(CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(r => r.is_active)
            .OrderBy(r => r.name)
            .ToListAsync(ct);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .AnyAsync(r => r.name == name, ct);
}
