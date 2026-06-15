using API.Contracts.Auth;
using Application.UseCases.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCustomerRequest request,
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

        if (string.IsNullOrWhiteSpace(request.FirstName))
        {
            return BadRequest(new { message = "El nombre es obligatorio." });
        }

        if (string.IsNullOrWhiteSpace(request.LastName))
        {
            return BadRequest(new { message = "El apellido es obligatorio." });
        }

        try
        {
            var command = new RegisterCustomerCommand
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone
            };

            var result = await _mediator.Send(command, ct);

            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
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
            var command = new LoginCustomerCommand
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