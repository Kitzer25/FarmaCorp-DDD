using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.Order.Querys;

public sealed record GenerateOrderNumberQuery : IRequest<string>;

public sealed class GenerateOrderNumberQueryHandler : IRequestHandler<GenerateOrderNumberQuery, string>
{
    private readonly IOrderService _service;

    public GenerateOrderNumberQueryHandler(IOrderService service)
    {
        _service = service;
    }

    public async Task<string> Handle(GenerateOrderNumberQuery query, CancellationToken ct)
    {
        return await _service.GenerateOrderNumberAsync(ct);
    }
}