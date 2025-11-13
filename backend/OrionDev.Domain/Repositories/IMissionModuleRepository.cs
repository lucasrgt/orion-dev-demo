using OrionDev.Domain.Entities;

namespace OrionDev.Domain.Repositories;

public interface IMissionModuleRepository {
  Task SaveAsync();
  Task<IEnumerable<MissionModule>> GetAllAsync();
  Task<MissionModule> GetByIdAsync(Guid id);
  Task AddAsync(MissionModule missionModule);
  Task DeleteByIdAsync(Guid id);
}
