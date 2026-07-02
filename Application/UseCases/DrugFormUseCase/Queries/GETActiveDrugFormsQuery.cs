using Domain.DTO_s.DrugForm;
using Domain.Ports.Services.EServices;
using MediatR;

namespace Application.UseCases.DrugFormUseCase.Queries;

public sealed record GETActiveDrugFormsQuery : IRequest<List<DrugFormDto>>;

public sealed class GETActiveDrugFormsQueryHandler : IRequestHandler<GETActiveDrugFormsQuery, List<DrugFormDto>>
{
    private readonly IDrugFormService _service;

    public GETActiveDrugFormsQueryHandler(IDrugFormService service)
    {
        _service = service;
    }

    public async Task<List<DrugFormDto>> Handle(GETActiveDrugFormsQuery query, CancellationToken ct)
    {
        return await _service.GetActiveAsync(ct);
    }
}
