using Application.Auth.Dtos;
using Core.Entities;
using Core.Ports;
using Infraestructure.Adapters.Services.AuthService;
using MediatR;

namespace Application.UseCases.Auth.Commands;

public class RegisterCustomerCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }
}

public sealed class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public RegisterCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower();

        var emailExists = await _unitOfWork.CustomerRepo.ExistsByEmailAsync(email, cancellationToken);

        if (emailExists)
        {
            throw new InvalidOperationException("El correo ya está registrado.");
        }

        var customer = new Customer
        {
            email = email,
            password_hash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            first_name = request.FirstName.Trim(),
            last_name = request.LastName.Trim(),
            phone = request.Phone,
            is_active = true,
            is_email_verified = false,
            created_at = DateTime.UtcNow
        };

        await _unitOfWork.CustomerRepo.AddAsync(customer, cancellationToken);

        var token = _jwtTokenGenerator.GenerateCustomerToken(customer);

        return new AuthResponseDto
        {
            Id = customer.customer_id,
            Email = customer.email,
            FullName = $"{customer.first_name} {customer.last_name}",
            Role = "Client",
            Token = token
        };
    }
}