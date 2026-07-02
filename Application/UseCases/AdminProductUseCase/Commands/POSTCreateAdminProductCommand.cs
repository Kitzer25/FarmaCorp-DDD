using Domain.DTO_s.AdminProducts;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.AdminProductUseCase.Commands;

public sealed class POSTCreateAdminProductCommand : IRequest<ProductAdminDto>
{
    public SaveProductAdminDto Request { get; init; } = null!;
}

public sealed class POSTCreateAdminProductCommandHandler : IRequestHandler<POSTCreateAdminProductCommand, ProductAdminDto>
{
    private readonly IAdminProductService _service;

    public POSTCreateAdminProductCommandHandler(IAdminProductService service)
    {
        _service = service;
    }

    public async Task<ProductAdminDto> Handle(POSTCreateAdminProductCommand command, CancellationToken ct)
    {
        return await _service.CreateAsync(command.Request, ct);
    }
}
