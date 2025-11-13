using Microsoft.EntityFrameworkCore;
using OrionDev.Core.Exceptions;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Repositories;
using OrionDev.Infrastructure.Data.Database;

namespace OrionDev.Infrastructure.Data.RepositoriesImpl;

public class MissionModuleRepository(AppDbContext context) : IMissionModuleRepository {
  public async Task SaveAsync() {
    await context.SaveChangesAsync();
  }

  public async Task<IEnumerable<MissionModule>> GetAllAsync() {
    var missionModules = await context.MissionModules.ToListAsync();
    if (missionModules == null) {
      throw new DbNotFoundException("No missionModules found");
    }

    return missionModules;
  }

  public async Task<MissionModule> GetByIdAsync(Guid id) {
    var missionModule = await context.MissionModules.FirstOrDefaultAsync(p => p.Id == id);
    if (missionModule == null) {
      throw new DbNotFoundException("No missionModule was found with this id");
    }

    return missionModule;
  }

  public async Task AddAsync(MissionModule missionModule) {
    await context.MissionModules.AddAsync(missionModule);
  }

  public async Task DeleteByIdAsync(Guid id) {
    var missionModule = await context.MissionModules.FirstOrDefaultAsync(p => p.Id == id);
    if (missionModule == null) {
      throw new DbNotFoundException("No missionModule was found with this id");
    }

    context.MissionModules.Remove(missionModule);
  }
}
