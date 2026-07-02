using System.Security.Claims;
using Domain.Entities;

namespace Domain.Ports.Services.AuthService;

public interface IJwtTokenGenerator
{
    public string GenerateCustomerToken(Customer customer);
    public string GenerateAdminToken(User user);
    public string GenerateToken(List<Claim> claims);
}