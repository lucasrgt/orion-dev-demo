using OrionDev.Domain.Entities;

namespace OrionDev.Domain.Repositories;

public interface IUserRepository {
  Task SaveAsync();
  Task AddAsync(User user);
  Task<User> GetByIdAsync(Guid id);
  Task<User> GetByEmailAsync(string email);
}
