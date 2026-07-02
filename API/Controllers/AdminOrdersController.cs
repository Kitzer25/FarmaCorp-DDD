using System.Security.Claims;
using Application.UseCases.OrderUseCase.Commands;
using Application.UseCases.OrderUseCase.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commons;
using Domain.DTO_s.Orders;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNextOrderNumber(CancellationToken ct)
    {
        var orderNumber = await _mediator.Send(new GETGenerateOrderNumberQuery(), ct);
        return Ok(new { orderNumber });
    }

    [HttpPatch("{orderId:int}/status")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangeStatus(int orderId, ChangeOrderStatusRequest request, CancellationToken ct)
    {
        if (request.StatusId <= 0)
        {
            return BadRequest(new { message = "El estado es obligatorio." });
        }

        var result = await _mediator.Send(new PATCHChangeOrderStatusCommand
        {
            OrderId = orderId,
            StatusId = request.StatusId,
            ChangedByUserId = GetUserId(),
            Notes = request.Notes
        }, ct);
        return Ok(result);
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
