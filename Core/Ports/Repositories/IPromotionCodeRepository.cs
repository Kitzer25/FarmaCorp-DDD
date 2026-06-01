using Core.Entities;

namespace Core.Ports.Repositories;

public interface IPromotionCodeRepository :
    IGRepositories<PromotionCode>
{
    Task<PromotionCode?> GetActiveByCodeWithPromotionAsync(string code, CancellationToken ct);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken ct);
}
