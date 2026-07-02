using Domain.DTO_s.Batches;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.BatchUseCase.Queries;

public sealed record GETBatchesQuery : IRequest<IEnumerable<ProductBatchDto>>;

public sealed class GETBatchesQueryHandler : IRequestHandler<GETBatchesQuery, IEnumerable<ProductBatchDto>>
{
    private readonly IBatchService _batchService;

    public GETBatchesQueryHandler(IBatchService batchService)
    {
        _batchService = batchService;
    }

    public async Task<IEnumerable<ProductBatchDto>> Handle(GETBatchesQuery query, CancellationToken ct)
    {
        return await _batchService.GetBatchesAsync(ct);
    }
}
