using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IPaymentMethodRepository :
    IGRepositories<PaymentMethod>
{
    Task<PaymentMethod?> GetByNameAsync(string name, CancellationToken ct);

    Task<IEnumerable<PaymentMethod>> GetAllActiveAsync(CancellationToken ct);

    Task<IEnumerable<PaymentMethod>> GetOnlineMethodsAsync(CancellationToken ct);

    Task<bool> ExistsByNameAsync(string name, CancellationToken ct);

}