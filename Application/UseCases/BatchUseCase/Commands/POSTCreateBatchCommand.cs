using Domain.DTO_s.Batches;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.BatchUseCase.Commands;

public sealed class POSTCreateBatchCommand : IRequest<ProductBatchDto>
{
    public CreateProductBatchDto Request { get; set; } = null!;
}

public sealed class POSTCreateBatchCommandHandler : IRequestHandler<POSTCreateBatchCommand, ProductBatchDto>
{
    private readonly IBatchService _batchService;

    public POSTCreateBatchCommandHandler(IBatchService batchService)
    {
        _batchService = batchService;
    }

    public async Task<ProductBatchDto> Handle(POSTCreateBatchCommand command, CancellationToken ct)
    {
        return await _batchService.CreateAsync(command.Request, ct);
    }
}
