using Application.UseCases.DiscountTypeUseCase.Queries;
using Domain.DTO_s.DiscountType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/discount-types")]
public sealed class DiscountTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<DiscountTypeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _mediator.Send(new GETDiscountTypesQuery(), ct);
        return Ok(result);
    }
}
