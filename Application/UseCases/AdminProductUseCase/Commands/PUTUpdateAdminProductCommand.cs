using Domain.DTO_s.AdminProducts;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.AdminProductUseCase.Commands;

public sealed class PUTUpdateAdminProductCommand : IRequest<ProductAdminDto>
{
    public int ProductId { get; init; }
    public SaveProductAdminDto Request { get; init; } = null!;
}

public sealed class PUTUpdateAdminProductCommandHandler : IRequestHandler<PUTUpdateAdminProductCommand, ProductAdminDto>
{
    private readonly IAdminProductService _service;

    public PUTUpdateAdminProductCommandHandler(IAdminProductService service)
    {
        _service = service;
    }

    public async Task<ProductAdminDto> Handle(PUTUpdateAdminProductCommand command, CancellationToken ct)
    {
        return await _service.UpdateAsync(command.ProductId, command.Request, ct);
    }
}
