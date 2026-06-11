namespace Application.Auth.Dtos;

public class AuthResponseDto
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Token { get; set; } = null!;
}