using Core.Entities;

namespace Core.Ports.Repositories.ERepository;

public interface IPromotionRepository :
    IGRepositories<Promotion>
{
    Task<IEnumerable<Promotion>> GetActiveAsync(CancellationToken ct);
}
