using Domain.DTO_s.Batches;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Batch.Querys;

public sealed record GetBatchesQuery : IRequest<IEnumerable<ProductBatchDto>>;

public sealed class GetBatchesQueryHandler : IRequestHandler<GetBatchesQuery, IEnumerable<ProductBatchDto>>
{
    private readonly IBatchService _batchService;

    public GetBatchesQueryHandler(IBatchService batchService)
    {
        _batchService = batchService;
    }

    public async Task<IEnumerable<ProductBatchDto>> Handle(GetBatchesQuery query, CancellationToken ct)
    {
        return await _batchService.GetBatchesAsync(ct);
    }
}
