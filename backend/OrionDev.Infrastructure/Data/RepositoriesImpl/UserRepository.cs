using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using OrionDev.Core.Messages;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Repositories;
using OrionDev.Infrastructure.Data.Database;

namespace OrionDev.Infrastructure.Data.RepositoriesImpl;

public class UserRepository(AppDbContext context) : IUserRepository {
  public async Task AddAsync(User user) {
    await context.Users.AddAsync(user);
  }

  public async Task<User> GetByEmailAsync(string email) {
    var user =
      await context.Users.FirstOrDefaultAsync(u => u.Email.Value == email)
      ?? throw new AuthenticationException(Messages.EmailNotFound);
    return user;
  }

  public async Task<User> GetByIdAsync(Guid id) {
    var user =
      await context.Users.FirstOrDefaultAsync(u => u.Id == id)
      ?? throw new AuthenticationException($"User with id {id} not found");
    return user;
  }

  public async Task SaveAsync() {
    await context.SaveChangesAsync();
  }
}
