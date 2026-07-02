using Domain.DTO_s.Batches;

namespace Domain.Ports.Services.EServices;

public interface IBatchService
{
    Task<IEnumerable<ProductBatchDto>> GetBatchesAsync(CancellationToken ct);
    Task<ProductBatchDto> CreateAsync(CreateProductBatchDto request, CancellationToken ct);
    Task<IEnumerable<ExpiringBatchDto>> GetExpiringBatchesAsync(CancellationToken ct);
}