using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OrionDev.Domain.Interfaces;
using OrionDev.Infrastructure.Settings;

namespace OrionDev.Infrastructure.Security.Auth;

public class JwtService(JwtSettings settings) : IJwtService {
  private static class CustomClaimTypes {
    public const string Id = "id";
  }

  public string CreateAccessToken(string id, string role) {
    var claims = new List<Claim> {
      new(ClaimTypes.Role, role),
      new(CustomClaimTypes.Id, id)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.AccessTokenSecret));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    var token = new JwtSecurityToken(
      claims: claims,
      expires: DateTime.Now.AddMinutes(settings.AccessTokenExpirationInMinutes),
      signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
