using OrionDev.Domain.Enums;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Interfaces;
using OrionDev.Domain.VOs;

namespace OrionDev.Infrastructure.Helpers;

public static class UserSeedHelper {
  public static User CreateAdmin(
    string email,
    string fullName,
    string nickName,
    string plainTextPassword,
    IPasswordEncryptionService encryptionService
  ) {
    return new User(
      Guid.NewGuid(),
      DateTime.UtcNow,
      DateTime.UtcNow,
      new Email(email),
      Password.CreateSecure(plainTextPassword, encryptionService),
      UserRole.Admin,
      fullName,
      nickName
    );
  }
}
