using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Auth.Services;

public class JwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateCustomerToken(Customer customer)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, customer.customer_id.ToString()),
            new Claim(ClaimTypes.Email, customer.email),
            new Claim(ClaimTypes.Name, $"{customer.first_name} {customer.last_name}"),
            new Claim(ClaimTypes.Role, "Client")
        };

        return GenerateToken(claims);
    }

    public string GenerateAdminToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.user_id.ToString()),
            new Claim(ClaimTypes.Email, user.email),
            new Claim(ClaimTypes.Name, $"{user.first_name} {user.last_name}")
        };

        foreach (var userRole in user.user_roleusers)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.role.name));
        }

        return GenerateToken(claims);
    }

    private string GenerateToken(List<Claim> claims)
    {
        var secretKey = _configuration["JwtSettings:SecretKey"]
            ?? throw new InvalidOperationException("SecretKey no encontrado");

        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];

        var expirationMinutes = int.Parse(
            _configuration["JwtSettings:ExpirationMinutes"] ?? "60"
        );

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}