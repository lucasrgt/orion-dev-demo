using OrionDev.Domain.Entities;

namespace OrionDev.Domain.Repositories;

public interface IMissionRepository {
  Task SaveAsync();
  Task<IEnumerable<Mission>> GetAllAsync();
  Task<Mission> GetByIdAsync(Guid id);
  Task AddAsync(Mission mission);
  Task DeleteByIdAsync(Guid id);
}
