using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class DiscountTypeRepository : 
    GRepositories<DiscountType>,
    IDiscountTypeRepository
{ 
    public DiscountTypeRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<DiscountType?> GetByNameAsync(string name, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(d => d.name == name, ct);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .AnyAsync(d => d.name == name, ct);

}
