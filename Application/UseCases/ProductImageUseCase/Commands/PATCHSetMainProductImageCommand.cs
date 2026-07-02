using Domain.DTO_s.ProductImage;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.ProductImageUseCase.Commands;

public sealed class PATCHSetMainProductImageCommand : IRequest<ProductImageDto>
{
    public int ProductImageId { get; set; }
}

public sealed class PATCHSetMainProductImageCommandHandler : IRequestHandler<PATCHSetMainProductImageCommand, ProductImageDto>
{
    private readonly IProductImageService _service;

    public PATCHSetMainProductImageCommandHandler(IProductImageService service)
    {
        _service = service;
    }

    public async Task<ProductImageDto> Handle(PATCHSetMainProductImageCommand command, CancellationToken ct)
    {
        return await _service.SetMainImageAsync(command.ProductImageId, ct);
    }
}
