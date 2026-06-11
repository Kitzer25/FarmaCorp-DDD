using Application.Batches.Dtos;

namespace Core.Ports.Services;

public interface IBatchService
{
    Task<IEnumerable<ProductBatchDto>> GetBatchesAsync(CancellationToken ct);
    Task<ProductBatchDto> CreateAsync(CreateProductBatchDto request, CancellationToken ct);
}