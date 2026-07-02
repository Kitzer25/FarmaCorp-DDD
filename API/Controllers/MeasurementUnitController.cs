using Application.UseCases.MeasurementUnitUseCase.Queries;
using Domain.DTO_s.MeasurementUnit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/measurement-units")]
public sealed class MeasurementUnitController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeasurementUnitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<MeasurementUnitDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        var result = await _mediator.Send(new GETActiveMeasurementUnitsQuery(), ct);
        return Ok(result);
    }
}
