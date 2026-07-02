using Domain.DTO_s.AdminProducts;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Product.Commands;

public sealed class CreateAdminProductCommand : IRequest<ProductAdminDto>
{
    public SaveProductAdminDto Request { get; init; } = null!;
}

public sealed class CreateAdminProductCommandHandler : IRequestHandler<CreateAdminProductCommand, ProductAdminDto>
{
    private readonly IAdminProductService _service;

    public CreateAdminProductCommandHandler(IAdminProductService service)
    {
        _service = service;
    }

    public async Task<ProductAdminDto> Handle(CreateAdminProductCommand command, CancellationToken ct)
    {
        return await _service.CreateAsync(command.Request, ct);
    }
}