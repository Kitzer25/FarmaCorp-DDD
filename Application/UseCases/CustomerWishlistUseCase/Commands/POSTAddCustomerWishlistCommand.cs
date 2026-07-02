using Domain.DTO_s.CustomerWhislist;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.CustomerWishlistUseCase.Commands;

public sealed class POSTAddCustomerWishlistCommand : IRequest<CustomerWishlistDto>
{
    public int CustomerId { get; set; }
    public int ProductVariantId { get; set; }
}

public sealed class POSTAddCustomerWishlistCommandHandler : IRequestHandler<POSTAddCustomerWishlistCommand, CustomerWishlistDto>
{
    private readonly ICustomerWishlistService _service;

    public POSTAddCustomerWishlistCommandHandler(ICustomerWishlistService service)
    {
        _service = service;
    }

    public async Task<CustomerWishlistDto> Handle(POSTAddCustomerWishlistCommand command, CancellationToken ct)
    {
        return await _service.AddAsync(command.CustomerId, command.ProductVariantId, ct);
    }
}
