using Microsoft.IdentityModel.Tokens;
using Norn.Models.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Norn.Api.Security;

public class BearerTokenGenerator : IBearerTokenGenerator
{
    private IConfiguration _configuration;
    public BearerTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user, string roleName)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, roleName)
        };
        var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        var creds = new SigningCredentials(
        key,
        SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
        issuer: _configuration["JwtIssuer"],
        audience: _configuration["JwtAudience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(12),
        signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);

    }

}
