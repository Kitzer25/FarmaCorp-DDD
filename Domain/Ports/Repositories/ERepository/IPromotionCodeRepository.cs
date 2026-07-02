using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IPromotionCodeRepository :
    IGRepositories<PromotionCode>
{
    Task<PromotionCode?> GetActiveByCodeWithPromotionAsync(string code, CancellationToken ct);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken ct);
}
