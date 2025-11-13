using OrionDev.Domain.Entities;

namespace OrionDev.Domain.Repositories;

public interface IExpeditionRepository {
  Task SaveAsync();
  Task<IEnumerable<Expedition>> GetAllAsync();
  Task<Expedition> GetByIdAsync(Guid id);
  Task AddAsync(Expedition expedition);
  Task DeleteByIdAsync(Guid id);
}
