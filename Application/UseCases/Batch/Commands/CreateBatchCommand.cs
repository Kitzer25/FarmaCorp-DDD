using Domain.DTO_s.Batches;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Batch.Commands;

public sealed class CreateBatchCommand : IRequest<ProductBatchDto>
{
    public CreateProductBatchDto Request { get; set; } = null!;
}

public sealed class CreateBatchCommandHandler : IRequestHandler<CreateBatchCommand, ProductBatchDto>
{
    private readonly IBatchService _batchService;

    public CreateBatchCommandHandler(IBatchService batchService)
    {
        _batchService = batchService;
    }

    public async Task<ProductBatchDto> Handle(CreateBatchCommand command, CancellationToken ct)
    {
        return await _batchService.CreateAsync(command.Request, ct);
    }
}
