using Application.Auth.Dtos;
using MediatR;

namespace Application.Auth.Commands;

public class LoginCustomerCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}