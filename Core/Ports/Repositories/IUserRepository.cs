using Core.Entities;

namespace Core.Ports.Repositories;

public interface IUserRepository :
    IGRepositories<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
}