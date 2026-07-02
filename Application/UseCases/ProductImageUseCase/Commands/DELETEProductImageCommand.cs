using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.ProductImageUseCase.Commands;

public sealed class DELETEProductImageCommand : IRequest<bool>
{
    public int ProductImageId { get; set; }
}

public sealed class DELETEProductImageCommandHandler : IRequestHandler<DELETEProductImageCommand, bool>
{
    private readonly IProductImageService _service;

    public DELETEProductImageCommandHandler(IProductImageService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(DELETEProductImageCommand command, CancellationToken ct)
    {
        return await _service.DeleteImageAsync(command.ProductImageId, ct);
    }
}
