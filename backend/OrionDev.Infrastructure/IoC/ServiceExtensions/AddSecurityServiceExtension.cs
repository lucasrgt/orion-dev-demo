using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrionDev.Domain.Interfaces;
using OrionDev.Infrastructure.Security.Auth;
using OrionDev.Infrastructure.Security.Cryptography;
using OrionDev.Infrastructure.Settings;

namespace OrionDev.Infrastructure.IoC.ServiceExtensions;

public static class SecurityServiceExtension {
  public static IServiceCollection AddSecurity(
    this IServiceCollection services,
    IConfiguration configuration
  ) {
    // Encryption
    services.AddScoped<IPasswordEncryptionService, PasswordEncryptionServiceBCrypt>();

    // Jwt
    services.AddScoped<IJwtService, JwtService>();
    services.AddScoped<IRefreshTokenService, RefreshTokenService>();

    var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
    if (jwtSettings is null) {
      throw new InvalidOperationException(
        "JwtSettings section is missing or malformed in appsettings.");
    }

    services.AddSingleton(jwtSettings);

    return services;
  }
}
