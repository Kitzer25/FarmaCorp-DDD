using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IPromotionRepository :
    IGRepositories<Promotion>
{
    Task<IEnumerable<Promotion>> GetActiveAsync(CancellationToken ct);
}
