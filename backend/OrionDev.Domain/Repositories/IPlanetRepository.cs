using OrionDev.Domain.Entities;

namespace OrionDev.Domain.Repositories;

public interface IPlanetRepository {
  Task SaveAsync();
  Task<IEnumerable<Planet>> GetAllAsync();
  Task<Planet> GetByIdAsync(Guid id);
  Task AddAsync(Planet planet);
  Task DeleteByIdAsync(Guid id);
}


