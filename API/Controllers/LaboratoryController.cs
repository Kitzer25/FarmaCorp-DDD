using Application.UseCases.LaboratoryUseCase.Queries;
using Domain.DTO_s.Laboratory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/laboratories")]
public sealed class LaboratoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public LaboratoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<LaboratoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetActiveLaboratoriesQuery(), ct);
        return Ok(result);
    }
}
