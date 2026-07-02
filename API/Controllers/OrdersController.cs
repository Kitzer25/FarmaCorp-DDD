using System.Security.Claims;
using Application.UseCases.OrderUseCase.Commands;
using Application.UseCases.OrderUseCase.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Commons;
using Domain.Ports.Services;
using Domain.DTO_s.Orders;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.ClientAccess)]
[Route("api/v1/orders")]
public sealed class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("next-number")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNextOrderNumber(CancellationToken ct)
    {
        var result = await _mediator.Send(new GETGenerateOrderNumberQuery(), ct);
        return Ok(new { OrderNumber = result });
    }

    [HttpPost("checkout/{customerId:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Checkout(int customerId, [FromBody] CheckoutRequestDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new POSTCheckoutCommand { CustomerId = customerId, Request = dto }, ct);
        return Ok(result);
    }

    [HttpPut("{orderId:int}/status")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeStatus(int orderId, [FromBody] ChangeStatusRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(new PATCHChangeOrderStatusCommand
        {
            OrderId = orderId, 
            StatusId = request.StatusId, 
            ChangedByUserId = request.ChangedByUserId, 
            Notes = request.Notes 
        }, ct);
        return Ok(result);
    }
}

public record ChangeStatusRequest(int StatusId, int? ChangedByUserId, string? Notes);
