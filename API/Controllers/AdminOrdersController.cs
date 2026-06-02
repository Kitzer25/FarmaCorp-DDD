using System.Security.Claims;
using Application.Orders.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Constants;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.SalesAccess)]
[Route("api/v1/admin/orders")]
public class AdminOrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public AdminOrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("next-number")]
    public async Task<IActionResult> GetNextOrderNumber(CancellationToken ct)
    {
        return Ok(new { orderNumber = await _orderService.GenerateOrderNumberAsync(ct) });
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
            var result = await _orderService.ChangeStatusAsync(orderId, request.StatusId, GetUserId(), request.Notes, ct);
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
