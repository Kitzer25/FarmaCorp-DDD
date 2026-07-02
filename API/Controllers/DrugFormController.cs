using Application.UseCases.DrugFormUseCase.Queries;
using Domain.DTO_s.DrugForm;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/drug-forms")]
public sealed class DrugFormController : ControllerBase
{
    private readonly IMediator _mediator;

    public DrugFormController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<DrugFormDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        var result = await _mediator.Send(new GETActiveDrugFormsQuery(), ct);
        return Ok(result);
    }
}
