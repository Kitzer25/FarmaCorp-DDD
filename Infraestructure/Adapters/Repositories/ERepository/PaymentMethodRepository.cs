using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;
public class PaymentMethodRepository : 
    GRepositories<PaymentMethod>,
    IPaymentMethodRepository
{ 
    public PaymentMethodRepository(AppDbContext context) : base(context)
    { }  
    
    public async Task<PaymentMethod?> GetByNameAsync(string name, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
           .FirstOrDefaultAsync(p => p.name == name, ct);

    public async Task<IEnumerable<PaymentMethod>> GetAllActiveAsync(CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(p => p.is_active)
            .OrderBy(p => p.name)
            .ToListAsync(ct);

    public async Task<IEnumerable<PaymentMethod>> GetOnlineMethodsAsync(CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .Where(p => p.is_active && p.is_online)
            .OrderBy(p => p.name)
            .ToListAsync(ct);

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct) =>
        await _dbSet.AsNoTracking()
            .AnyAsync(p => p.name == name, ct);
}
