// PulsePro.Persistence/Security/JwtGenerator.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration; 
using PulsePro.Application.Abstraction;

namespace PulsePro.Persistence.Security;
public sealed class JwtGenerator : ITokenGenerator
{
    private readonly JwtSecurityTokenHandler _handler = new();
    private readonly byte[] _key;   //  беремо з  appsettings

    public JwtGenerator(IConfiguration cfg)
    {
        _key = Convert.FromBase64String(cfg["Jwt:Key"]);
    }

    public string GenerateAccessToken(Guid userId, string email)
    {
        var creds = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email)
            },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return _handler.WriteToken(token);
    }
}