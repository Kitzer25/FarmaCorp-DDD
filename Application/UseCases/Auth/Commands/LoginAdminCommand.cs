using Application.Auth.Dtos;
using Core.Ports;
using Core.Ports.Services.AuthService;
using MediatR;

namespace Application.UseCases.Auth.Commands;

public class LoginAdminCommand : IRequest<AuthResponseDto>
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}


public sealed class LoginAdminCommandHandler : IRequestHandler<LoginAdminCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IHashPassword _hashPassword;

    public LoginAdminCommandHandler(
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator,
        IHashPassword hashPassword)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _hashPassword = hashPassword;
    }

    public async Task<AuthResponseDto> Handle(LoginAdminCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLower();

        var user = await _unitOfWork.UserRepo.GetByEmailAsync(email, cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        if (!user.is_active)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }
        
        // TODO revisión si falla
        var passwordOk = _hashPassword.Verify(user.password_hash, request.Password);;

        if (!passwordOk)
        {
            throw new UnauthorizedAccessException("Credenciales inválidas.");
        }

        user.last_login_at = DateTime.UtcNow;

        await _unitOfWork.UserRepo.UpdateAsync(user.user_id, user, cancellationToken);

        var token = _jwtTokenGenerator.GenerateAdminToken(user);

        var role = user.user_roleusers.FirstOrDefault()?.role.name ?? "User";

        return new AuthResponseDto
        {
            Id = user.user_id,
            Email = user.email,
            FullName = $"{user.first_name} {user.last_name}",
            Role = role,
            Token = token
        };
    }
}