using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.OrderUseCase.Queries;

public sealed record GETGenerateOrderNumberQuery : IRequest<string>;

public sealed class GETGenerateOrderNumberQueryHandler : IRequestHandler<GETGenerateOrderNumberQuery, string>
{
    private readonly IOrderService _service;

    public GETGenerateOrderNumberQueryHandler(IOrderService service)
    {
        _service = service;
    }

    public async Task<string> Handle(GETGenerateOrderNumberQuery query, CancellationToken ct)
    {
        return await _service.GenerateOrderNumberAsync(ct);
    }
}
