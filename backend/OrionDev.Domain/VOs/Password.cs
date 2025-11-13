using System.Text.RegularExpressions;
using OrionDev.Core.Exceptions;
using OrionDev.Core.Messages;
using OrionDev.Core.Validators;
using OrionDev.Domain.Interfaces;

namespace OrionDev.Domain.VOs;

public partial class Password {
  public string Value { get; private set; }

  private Password() {
  }

  private Password(string password) {
    Value = password;
  }

  /// <summary>
  /// Creates a new secure instance of <see cref="Password"/> by validating the minimum strength
  /// and encrypting the input using the provided encryption service.
  /// </summary>
  /// <param name="password">The plain-text password to validate and encrypt.</param>
  /// <param name="passwordEncryptionService">The encryption service used to hash the password.</param>
  /// <returns>A <see cref="Password"/> instance containing the encrypted hash.</returns>
  /// <exception cref="DomainValidationException">
  /// Thrown when the password is invalid or does not meet the required complexity.
  /// </exception>
  public static Password CreateSecure(
    string password,
    IPasswordEncryptionService passwordEncryptionService
  ) {
    var validPassword = StringValidator.Validate(password, "Password", 8, 255);
    if (!PasswordRegex().IsMatch(validPassword)) {
      throw new DomainValidationException(Messages.InsecurePassword);
    }

    var passwordHash = passwordEncryptionService.Hash(validPassword);
    return new Password(passwordHash);
  }

  /// <summary>
  /// Creates a <see cref="Password"/> instance from a pre-hashed password value, such as one retrieved from the database.
  /// No validation or encryption is applied.
  /// </summary>
  /// <param name="passwordHash">The already encrypted password hash.</param>
  /// <returns>A <see cref="Password"/> instance encapsulating the provided hash.</returns>
  public static Password FromHash(string passwordHash) {
    return new Password(passwordHash);
  }

  /// <summary>
  /// Verifies whether the provided plain-text password matches the stored hash
  /// using the specified encryption service.
  /// </summary>
  /// <param name="plainTextPassword">The plain-text password to verify.</param>
  /// <param name="encryptionService">The encryption service responsible for hash comparison.</param>
  /// <returns><c>true</c> if the password matches the hash; otherwise, <c>false</c>.</returns>
  public bool IsMatch(string plainTextPassword, IPasswordEncryptionService encryptionService) {
    return encryptionService.Verify(Value, plainTextPassword);
  }

  /// <summary>
  /// Regular expression to enforce minimum password security requirements:
  /// at least one uppercase letter, one lowercase letter, one digit,
  /// and one special character.
  /// </summary>
  [GeneratedRegex(
    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).+$",
    RegexOptions.Compiled
  )]
  private static partial Regex PasswordRegex();
}
