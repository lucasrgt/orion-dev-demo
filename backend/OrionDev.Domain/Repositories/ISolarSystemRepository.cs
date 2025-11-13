using OrionDev.Domain.Entities;

namespace OrionDev.Domain.Repositories;

public interface ISolarSystemRepository {
  Task SaveAsync();
  Task<IEnumerable<SolarSystem>> GetAllAsync();
  Task<SolarSystem> GetByIdAsync(Guid id);
  Task AddAsync(SolarSystem solarSystem);
  Task DeleteByIdAsync(Guid id);
}
