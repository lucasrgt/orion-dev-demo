using System.Security.Authentication;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Interfaces;
using OrionDev.Domain.Repositories;
using OrionDev.Infrastructure.Settings;

namespace OrionDev.Infrastructure.Security.Auth;

public class RefreshTokenService(
  IRefreshTokenRepository refreshTokenRepository,
  IUserRepository userRepository,
  JwtSettings jwtSettings
) : IRefreshTokenService {
  public async Task<RefreshToken> CreateRefreshToken(string userId) {
    var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    var expires = DateTime.UtcNow.AddMinutes(jwtSettings.RefreshTokenExpirationInMinutes);
    var refreshToken = new RefreshToken(
      Guid.Parse(userId),
      token,
      expires
    );
    await refreshTokenRepository.SaveRefreshToken(refreshToken);
    return refreshToken;
  }

  public async Task<bool> ValidateRefreshToken(string token) {
    var refreshToken = await refreshTokenRepository.GetRefreshToken(token);
    return refreshToken is { IsValid: true };
  }

  public async Task<User> GetUserByRefreshToken(string token) {
    var refreshToken = await refreshTokenRepository.GetRefreshToken(token);
    if (refreshToken is not { IsValid: true }) {
      throw new AuthenticationException("Invalid or expired refresh token.");
    }

    var user =
      await userRepository.GetByIdAsync(refreshToken.UserId)
      ?? throw new AuthenticationException("User not found.");

    return user;
  }

  public async Task InvalidateRefreshToken(string token) {
    await refreshTokenRepository.DeleteRefreshToken(token);
  }
}
