using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Promotion.Querys;
using Application.UseCases.Promotion.Commands;
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
    public async Task<IActionResult> GetActive(CancellationToken ct)
    {
        return Ok(await _mediator.Send(new GetActivePromotionsQuery(), ct));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePromotionDto request, CancellationToken ct)
    {
        return Ok(await _mediator.Send(new CreatePromotionCommand { Request = request }, ct));
    }
}
