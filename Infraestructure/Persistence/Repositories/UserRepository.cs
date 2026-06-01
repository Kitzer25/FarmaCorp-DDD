using Core.Entities;
using Core.Ports.Repositories;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories;

public class UserRepository : 
    GRepositories<User>,
    IUserRepository
{ 
    public UserRepository(AppDbContext context) : base(context)
    { }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await _context.users1
            .Include(u => u.user_roleusers)
            .ThenInclude(ur => ur.role)
            .FirstOrDefaultAsync(u => u.email == email && u.deleted_at == null, ct);
    }
}