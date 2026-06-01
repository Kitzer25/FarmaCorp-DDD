using Application.Auth.Dtos;
using MediatR;

namespace Application.Auth.Commands;

public class RegisterCustomerCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }
}