using Application.Auth.Commands;
using Application.Auth.Dtos;
using Application.Auth.Services;
using Core.Ports;
using MediatR;

namespace Application.Auth.Handlers;

public class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public LoginCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower();

        var customer = await _unitOfWork.CustomerRepo.GetByEmailAsync(email, cancellationToken);

        if (customer == null)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        if (!customer.is_active)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        var passwordOk = BCrypt.Net.BCrypt.Verify(request.Password, customer.password_hash);

        if (!passwordOk)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        customer.last_login_at = DateTime.UtcNow;

        await _unitOfWork.CustomerRepo.UpdateAsync(customer.customer_id, customer, cancellationToken);

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