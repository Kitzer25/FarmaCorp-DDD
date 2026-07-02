using Domain.DTO_s.Laboratory;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.LaboratoryUseCase.Queries;

public sealed record GetActiveLaboratoriesQuery : IRequest<List<LaboratoryDto>>;

public sealed class GetActiveLaboratoriesQueryHandler : IRequestHandler<GetActiveLaboratoriesQuery, List<LaboratoryDto>>
{
    private readonly ILaboratoryService _service;

    public GetActiveLaboratoriesQueryHandler(ILaboratoryService service)
    {
        _service = service;
    }

    public async Task<List<LaboratoryDto>> Handle(GetActiveLaboratoriesQuery query, CancellationToken ct)
    {
        return await _service.GetActiveAsync(ct);
    }
}
