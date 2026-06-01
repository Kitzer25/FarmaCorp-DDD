using API.Contracts.Auth;
using Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/admin/auth")]
public class AdminAuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminAuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
        {
            return BadRequest(new { message = "El correo es obligatorio." });
        }

        if (string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new { message = "La contraseña es obligatoria." });
        }

        try
        {
            var command = new LoginAdminCommand
            {
                Email = request.Email,
                Password = request.Password
            };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Credenciales inválidas." });
        }
    }
}