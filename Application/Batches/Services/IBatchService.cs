using Application.Batches.Dtos;

namespace Application.Batches.Services;

public interface IBatchService
{
    Task<IEnumerable<ProductBatchDto>> GetBatchesAsync(CancellationToken ct);
    Task<ProductBatchDto> CreateAsync(CreateProductBatchDto request, CancellationToken ct);
}
