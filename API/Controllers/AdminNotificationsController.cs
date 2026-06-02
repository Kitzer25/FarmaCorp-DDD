using Application.Notifications.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Constants;

namespace API.Controllers;

[ApiController]
[Authorize(Policy = PolicyNames.AdminOnly)]
[Route("api/v1/admin/notifications")]
public class AdminNotificationsController : ControllerBase
{
    private readonly IEmailService _emailService;

    public AdminNotificationsController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("order-confirmation/test")]
    public async Task<IActionResult> TestOrderConfirmation(TestOrderEmailRequest request, CancellationToken ct)
    {
        await _emailService.SendOrderConfirmationAsync(request.Email, request.OrderNumber, request.Total, ct);

        return Ok(new { message = "Correo simulado registrado en consola." });
    }
}

public class TestOrderEmailRequest
{
    public string Email { get; set; } = null!;
    public string OrderNumber { get; set; } = null!;
    public decimal Total { get; set; }
}
