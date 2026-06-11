using Core.Entities;

namespace Core.Ports.Repositories.ERepository;

public interface IProductBatchRepository :
    IGRepositories<ProductBatch>
{
    Task<IEnumerable<ProductBatch>> GetAllWithProductAsync(CancellationToken ct);
    Task<ProductBatch?> GetByVariantAndBatchNumberAsync(int productVariantId, string batchNumber, CancellationToken ct);
}
