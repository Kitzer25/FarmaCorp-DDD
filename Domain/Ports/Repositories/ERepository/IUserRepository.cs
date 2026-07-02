using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IUserRepository :
    IGRepositories<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken ct);
}