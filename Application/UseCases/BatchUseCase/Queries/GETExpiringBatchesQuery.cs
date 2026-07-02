using Domain.DTO_s.Batches;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.BatchUseCase.Queries;

public sealed record GETExpiringBatchesQuery : IRequest<IEnumerable<ExpiringBatchDto>>;

public sealed class GETExpiringBatchesQueryHandler : IRequestHandler<GETExpiringBatchesQuery, IEnumerable<ExpiringBatchDto>>
{
    private readonly IBatchService _batchService;

    public GETExpiringBatchesQueryHandler(IBatchService batchService)
    {
        _batchService = batchService;
    }

    public async Task<IEnumerable<ExpiringBatchDto>> Handle(GETExpiringBatchesQuery query, CancellationToken ct)
    {
        return await _batchService.GetExpiringBatchesAsync(ct);
    }
}
