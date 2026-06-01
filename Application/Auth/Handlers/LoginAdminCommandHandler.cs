using Application.Auth.Commands;
using Application.Auth.Dtos;
using Application.Auth.Services;
using Core.Ports;
using MediatR;

namespace Application.Auth.Handlers;

public class LoginAdminCommandHandler : IRequestHandler<LoginAdminCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public LoginAdminCommandHandler(
        IUnitOfWork unitOfWork,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
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

        var passwordOk = BCrypt.Net.BCrypt.Verify(request.Password, user.password_hash);

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