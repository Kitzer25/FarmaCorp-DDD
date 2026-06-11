using Core.Ports.Services.AuthService;

namespace Infraestructure.Adapters.Services.AuthService;

public class HashPassword : IHashPassword
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string hash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}