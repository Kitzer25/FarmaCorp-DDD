using Core.DTO_s.Batches;

namespace Core.Ports.Services.EServices;

public interface IBatchService
{
    Task<IEnumerable<ProductBatchDto>> GetBatchesAsync(CancellationToken ct);
    Task<ProductBatchDto> CreateAsync(CreateProductBatchDto request, CancellationToken ct);
}