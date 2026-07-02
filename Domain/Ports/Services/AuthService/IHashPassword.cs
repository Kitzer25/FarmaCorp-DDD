namespace Domain.Ports.Services.AuthService;

public interface IHashPassword
{
    public string Hash(string password);
    public bool Verify(string hash, string password);
}