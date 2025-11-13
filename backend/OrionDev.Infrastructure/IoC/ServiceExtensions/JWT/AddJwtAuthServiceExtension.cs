using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace OrionDev.Infrastructure.IoC.ServiceExtensions.JWT;

public static class AddJwtAuthServiceExtension {
  public static IServiceCollection AddJwtAuth(
    this IServiceCollection services,
    IConfiguration configuration
  ) {
    var secret = configuration.GetSection("JwtSettings:AccessTokenSecret").Value!;
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
          ValidateAudience = false,
          ValidateIssuer = false
        };
      });

    services.AddAuthorization();

    return services;
  }
}
