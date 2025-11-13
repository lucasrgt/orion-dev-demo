using Microsoft.EntityFrameworkCore;
using OrionDev.Core.Exceptions;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Repositories;
using OrionDev.Infrastructure.Data.Database;

namespace OrionDev.Infrastructure.Data.RepositoriesImpl;

public class SolarSystemRepository(AppDbContext context) : ISolarSystemRepository {
  public async Task SaveAsync() {
    await context.SaveChangesAsync();
  }

  public async Task<IEnumerable<SolarSystem>> GetAllAsync() {
    var solarSystems = await context.SolarSystems.ToListAsync();
    if (solarSystems == null) {
      throw new DbNotFoundException("No solarSystems found");
    }

    return solarSystems;
  }

  public async Task<SolarSystem> GetByIdAsync(Guid id) {
    var solarSystem = await context.SolarSystems.FirstOrDefaultAsync(ss => ss.Id == id);
    if (solarSystem == null) {
      throw new DbNotFoundException("No solarSystem was found with this id");
    }

    return solarSystem;
  }

  public async Task AddAsync(SolarSystem solarSystem) {
    await context.SolarSystems.AddAsync(solarSystem);
  }

  public async Task DeleteByIdAsync(Guid id) {
    var solarSystem = await context.SolarSystems.FirstOrDefaultAsync(ss => ss.Id == id);
    if (solarSystem == null) {
      throw new DbNotFoundException("No solarSystem was found with this id");
    }

    context.SolarSystems.Remove(solarSystem);
  }
}
