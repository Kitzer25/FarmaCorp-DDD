using Domain.Entities;

namespace Domain.Ports.Repositories.ERepository;

public interface IProductBatchRepository :
    IGRepositories<ProductBatch>
{
    Task<IEnumerable<ProductBatch>> GetAllWithProductAsync(CancellationToken ct);
    Task<ProductBatch?> GetByVariantAndBatchNumberAsync(int productVariantId, string batchNumber, CancellationToken ct);
}
