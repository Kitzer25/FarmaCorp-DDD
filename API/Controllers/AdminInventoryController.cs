using System.Security.Claims;
using Application.UseCases.Inventory.Commands;
using Application.UseCases.Inventory.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Ports.Services;
using Domain.DTO_s.Inventory;
using MediatR;

namespace API.Controllers;

[ApiController]
[Route("api/v1/admin/inventory")]
public sealed class InventoryAdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public InventoryAdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetInventory(CancellationToken ct)
    {
        var result = await _mediator.Send(new GetInventoryQuery(), ct);
        return Ok(result);
    }

    [HttpPost("movements")]
    public async Task<IActionResult> RegisterMovement([FromQuery] int? userId, [FromBody] CreateInventoryMovementDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new RegisterInventoryMovementCommand { UserId = userId, Request = dto }, ct);
        return Ok(result);
    }
}