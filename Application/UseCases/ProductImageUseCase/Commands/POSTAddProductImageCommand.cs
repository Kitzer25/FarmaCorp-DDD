using Domain.DTO_s.ProductImage;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.ProductImageUseCase.Commands;

public sealed class POSTAddProductImageCommand : IRequest<ProductImageDto>
{
    public CreateProductImageDto Request { get; set; } = null!;
}

public sealed class POSTAddProductImageCommandHandler : IRequestHandler<POSTAddProductImageCommand, ProductImageDto>
{
    private readonly IProductImageService _service;

    public POSTAddProductImageCommandHandler(IProductImageService service)
    {
        _service = service;
    }

    public async Task<ProductImageDto> Handle(POSTAddProductImageCommand command, CancellationToken ct)
    {
        return await _service.AddImageAsync(command.Request, ct);
    }
}
