using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.AdminProductUseCase.Commands;

public sealed record DELETESoftDeleteAdminProductCommand(int ProductId) : IRequest;

public sealed class DELETESoftDeleteAdminProductCommandHandler : IRequestHandler<DELETESoftDeleteAdminProductCommand>
{
    private readonly IAdminProductService _service;

    public DELETESoftDeleteAdminProductCommandHandler(IAdminProductService service)
    {
        _service = service;
    }

    public async Task Handle(DELETESoftDeleteAdminProductCommand command, CancellationToken ct)
    {
        await _service.SoftDeleteAsync(command.ProductId, ct);
    }
}
