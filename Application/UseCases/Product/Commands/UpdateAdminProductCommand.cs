using Domain.DTO_s.AdminProducts;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Product.Commands;

public sealed class UpdateAdminProductCommand : IRequest<ProductAdminDto>
{
    public int ProductId { get; init; }
    public SaveProductAdminDto Request { get; init; } = null!;
}

public sealed class UpdateAdminProductCommandHandler : IRequestHandler<UpdateAdminProductCommand, ProductAdminDto>
{
    private readonly IAdminProductService _service;

    public UpdateAdminProductCommandHandler(IAdminProductService service)
    {
        _service = service;
    }

    public async Task<ProductAdminDto> Handle(UpdateAdminProductCommand command, CancellationToken ct)
    {
        return await _service.UpdateAsync(command.ProductId, command.Request, ct);
    }
}