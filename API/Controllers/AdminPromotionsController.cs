using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.PromotionUseCase.Queries;
using Application.UseCases.PromotionUseCase.Commands;
using Domain.Commons;
using Domain.DTO_s.Promotions;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ManagerAccess)]
[Route("api/v1/admin/promotions")]
public class AdminPromotionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminPromotionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PromotionDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        return Ok(await _mediator.Send(new GETActivePromotionsQuery(), ct));
    }

    [HttpPost]
    [ProducesResponseType(typeof(PromotionDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreatePromotionDto request, CancellationToken ct)
    {
        return Ok(await _mediator.Send(new POSTCreatePromotionCommand { Request = request }, ct));
    }
}
