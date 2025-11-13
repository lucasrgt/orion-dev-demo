using OrionDev.Domain.Entities;
using OrionDev.Domain.Enums;
using OrionDev.Domain.Interfaces;
using OrionDev.Domain.Repositories;
using OrionDev.Domain.VOs;

namespace OrionDev.Application.UseCases.Auth;

public class RegisterUser(
  IUserRepository repository,
  IPasswordEncryptionService passwordEncryptionService) {
  public async Task<RegisterUserOutput> Execute(RegisterUserInput input) {
    var password = Password.CreateSecure(input.Password, passwordEncryptionService);

    var user = new User(
      Guid.NewGuid(),
      DateTime.UtcNow,
      DateTime.UtcNow,
      new Email(input.Email),
      password,
      UserRole.User,
      input.FullName,
      input.NickName
    );

    await repository.AddAsync(user);
    await repository.SaveAsync();

    return new RegisterUserOutput(
      user.Id.ToString(),
      user.Role.ToString()
    );
  }
}

public record RegisterUserInput(
  string Email,
  string Password,
  string FullName,
  string NickName
);

public record RegisterUserOutput(string Id, string Role);
