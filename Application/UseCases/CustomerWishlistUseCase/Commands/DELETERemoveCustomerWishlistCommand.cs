using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CustomerWishlistUseCase.Commands;

public sealed class DELETERemoveCustomerWishlistCommand : IRequest<bool>
{
    public int CustomerId { get; set; }
    public int ProductVariantId { get; set; }
}

public sealed class DELETERemoveCustomerWishlistCommandHandler : IRequestHandler<DELETERemoveCustomerWishlistCommand, bool>
{
    private readonly ICustomerWishlistService _service;

    public DELETERemoveCustomerWishlistCommandHandler(ICustomerWishlistService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(DELETERemoveCustomerWishlistCommand command, CancellationToken ct)
    {
        return await _service.RemoveAsync(command.CustomerId, command.ProductVariantId, ct);
    }
}
