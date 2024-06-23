using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Security;

namespace RailwayManagementSystem.Infrastructure.Auth;

internal sealed class Authenticator(IOptions<AuthOptions> options) : IAuthenticator
{
    private readonly string _issuer = options.Value.Issuer;
    private readonly TimeSpan _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
    private readonly string _audience = options.Value.Audience;
    private readonly SigningCredentials _signingCredentials = new(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(options.Value.SigningKey)),
        SecurityAlgorithms.HmacSha256);
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new();

    public JwtDto CreateToken(Guid userId, string role)
    {
        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        ];

        var expires = DateTime.UtcNow.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, DateTime.UtcNow, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new JwtDto
        {
            AccessToken = token
        };
    }
}