using System.Security.Claims;
using Application.UseCases.InventoryUseCase.Commands;
using Application.UseCases.InventoryUseCase.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commons;
using Domain.Ports.Services;
using Domain.DTO_s.Inventory;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.SalesAccess)]
[Route("api/v1/admin/inventory")]
public sealed class AdminInventoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminInventoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<InventoryItemDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetInventory(CancellationToken ct)
    {
        var result = await _mediator.Send(new GETInventoryQuery(), ct);
        return Ok(result);
    }

    [HttpPost("movements")]
    [ProducesResponseType(typeof(InventoryItemDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterMovement([FromQuery] int? userId, [FromBody] CreateInventoryMovementDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new PostRegisterInventoryMovementCommand { UserId = userId, Request = dto }, ct);
        return Ok(result);
    }
}