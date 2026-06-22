using Core.DTO_s.Auth;
using Core.Ports;
using Core.Ports.Repositories;
using Core.Ports.Services.AuthService;
using Infraestructure.Adapters.Services.AuthService;
using MediatR;

namespace Application.UseCases.Auth.Commands;

public class LoginCustomerCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}

public sealed class LoginCustomerCommandHandler : IRequestHandler<LoginCustomerCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHashPassword _hashPassword;

    public LoginCustomerCommandHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator,
        IHashPassword hashPassword)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _hashPassword = hashPassword;
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
        
        // TODO Modificación - revisión si falla 
        var passwordOk = _hashPassword.Verify(customer.password_hash, request.Password);

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