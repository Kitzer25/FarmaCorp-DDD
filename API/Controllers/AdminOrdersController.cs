using System.Security.Claims;
using Application.UseCases.Order.Commands;
using Application.UseCases.Order.Querys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commons;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.SalesAccess)]
[Route("api/v1/admin/orders")]
public class AdminOrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminOrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("next-number")]
    public async Task<IActionResult> GetNextOrderNumber(CancellationToken ct)
    {
        var orderNumber = await _mediator.Send(new GenerateOrderNumberQuery(), ct);
        return Ok(new { orderNumber });
    }

    [HttpPatch("{orderId:int}/status")]
    public async Task<IActionResult> ChangeStatus(int orderId, ChangeOrderStatusRequest request, CancellationToken ct)
    {
        if (request.StatusId <= 0)
        {
            return BadRequest(new { message = "El estado es obligatorio." });
        }

        try
        {
            var result = await _mediator.Send(new ChangeOrderStatusCommand
            {
                OrderId = orderId,
                StatusId = request.StatusId,
                ChangedByUserId = GetUserId(),
                Notes = request.Notes
            }, ct);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    private int? GetUserId()
    {
        var claimValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return int.TryParse(claimValue, out var userId) ? userId : null;
    }
}

public class ChangeOrderStatusRequest
{
    public int StatusId { get; set; }
    public string? Notes { get; set; }
}
