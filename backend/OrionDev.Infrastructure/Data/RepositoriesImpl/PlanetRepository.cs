using Microsoft.EntityFrameworkCore;
using OrionDev.Core.Exceptions;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Repositories;
using OrionDev.Infrastructure.Data.Database;

namespace OrionDev.Infrastructure.Data.RepositoriesImpl;

public class PlanetRepository(AppDbContext context) : IPlanetRepository {
  public async Task SaveAsync() {
    await context.SaveChangesAsync();
  }

  public async Task<IEnumerable<Planet>> GetAllAsync() {
    var planets = await context.Planets.ToListAsync();
    if (planets == null) {
      throw new DbNotFoundException("No planets found");
    }

    return planets;
  }

  public async Task<Planet> GetByIdAsync(Guid id) {
    var planet = await context.Planets.FirstOrDefaultAsync(p => p.Id == id);
    if (planet == null) {
      throw new DbNotFoundException("No planet was found with this id");
    }

    return planet;
  }

  public async Task AddAsync(Planet planet) {
    await context.Planets.AddAsync(planet);
  }

  public async Task DeleteByIdAsync(Guid id) {
    var planet = await context.Planets.FirstOrDefaultAsync(p => p.Id == id);
    if (planet == null) {
      throw new DbNotFoundException("No planet was found with this id");
    }

    context.Planets.Remove(planet);
  }
}
