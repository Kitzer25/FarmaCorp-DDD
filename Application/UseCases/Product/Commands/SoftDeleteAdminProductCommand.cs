using Core.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Product.Commands;

public sealed record SoftDeleteAdminProductCommand(int ProductId) : IRequest;

public sealed class SoftDeleteAdminProductCommandHandler : IRequestHandler<SoftDeleteAdminProductCommand>
{
    private readonly IAdminProductService _service;

    public SoftDeleteAdminProductCommandHandler(IAdminProductService service)
    {
        _service = service;
    }

    public async Task Handle(SoftDeleteAdminProductCommand command, CancellationToken ct)
    {
        await _service.SoftDeleteAsync(command.ProductId, ct);
    }
}