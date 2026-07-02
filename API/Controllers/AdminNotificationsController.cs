using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Notification.Commands;
using Domain.Commons;
using MediatR;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.AdminOnly)]
[Route("api/v1/admin/notifications")]
public class AdminNotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminNotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("order-confirmation/test")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> TestOrderConfirmation(TestOrderEmailRequest request, CancellationToken ct)
    {
        await _mediator.Send(new POSTSendOrderConfirmationCommand
        {
            Email = request.Email,
            OrderNumber = request.OrderNumber,
            Total = request.Total
        }, ct);

        return Ok(new { message = "Correo simulado registrado en consola." });
    }
}

public class TestOrderEmailRequest
{
    public string Email { get; set; } = null!;
    public string OrderNumber { get; set; } = null!;
    public decimal Total { get; set; }
}
