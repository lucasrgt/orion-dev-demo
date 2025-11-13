using System.Security.Authentication;
using OrionDev.Core.Messages;
using OrionDev.Domain.Interfaces;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Auth;

public class Login(
  IUserRepository userRepository,
  IPasswordEncryptionService passwordEncryptionService
) {
  public async Task<LoginOutput> Execute(LoginInput input) {
    var user = await userRepository.GetByEmailAsync(input.Email);
    if (!user.Password.IsMatch(input.Password, passwordEncryptionService)) {
      throw new AuthenticationException(Messages.InvalidCredentials);
    }

    return new LoginOutput(
      user.Id.ToString(),
      user.Role.ToString()
    );
  }
}

public record LoginInput(string Email, string Password);

public record LoginOutput(string Id, string Role);
