namespace OrionDev.Infrastructure.Settings;

public record JwtSettings(
  string AccessTokenSecret,
  int AccessTokenExpirationInMinutes,
  int RefreshTokenExpirationInMinutes
);
