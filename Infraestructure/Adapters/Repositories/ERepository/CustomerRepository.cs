using Core.Entities;
using Core.Ports.Repositories;
using Core.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class CustomerRepository : 
    GRepositories<Customer>,
    ICustomerRepository
{ 
    public CustomerRepository(AppDbContext context) : base(context)
    { }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await _context.customers
            .FirstOrDefaultAsync(c => c.email == email && c.deleted_at == null, ct);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken ct)
    {
        return await _context.customers
            .AnyAsync(c => c.email == email && c.deleted_at == null, ct);
    }
}