using Domain.Entities;
using Domain.Ports.Repositories.ERepository;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Adapters.Repositories.ERepository;

public class CustomerAddressRepository : 
    GRepositories<CustomerAddress>,
    ICustomerAddressRepository
{ 
    public CustomerAddressRepository(AppDbContext context) : base(context)
    { }

    public async Task<IEnumerable<CustomerAddress>> GetActiveByCustomerIdAsync(int customerId, CancellationToken ct)
    {
        return await _context.customer_addresses
            .AsNoTracking()
            .Where(a => a.customer_id == customerId && a.is_active)
            .OrderByDescending(a => a.is_default)
            .ThenByDescending(a => a.updated_at ?? a.created_at)
            .ToListAsync(ct);
    }

    public async Task<CustomerAddress?> GetActiveByIdAndCustomerIdAsync(int addressId, int customerId, CancellationToken ct)
    {
        return await _context.customer_addresses
            .FirstOrDefaultAsync(a => a.address_id == addressId && a.customer_id == customerId && a.is_active, ct);
    }

    public async Task ClearDefaultForCustomerAsync(int customerId, int? exceptAddressId, CancellationToken ct)
    {
        var addresses = await _context.customer_addresses
            .Where(a => a.customer_id == customerId && a.is_active && a.is_default)
            .Where(a => exceptAddressId == null || a.address_id != exceptAddressId)
            .ToListAsync(ct);

        foreach (var address in addresses)
        {
            address.is_default = false;
            address.updated_at = DateTime.UtcNow;
        }
    }
}
