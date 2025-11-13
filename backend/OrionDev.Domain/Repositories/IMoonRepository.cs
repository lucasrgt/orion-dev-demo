using OrionDev.Domain.Entities;

namespace OrionDev.Domain.Repositories;

public interface IMoonRepository {
  Task SaveAsync();
  Task<IEnumerable<Moon>> GetAllAsync();
  Task<Moon> GetByIdAsync(Guid id);
  Task AddAsync(Moon moon);
  Task DeleteByIdAsync(Guid id);
}
