using Microsoft.EntityFrameworkCore;
using OrionDev.Core.Exceptions;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Repositories;
using OrionDev.Infrastructure.Data.Database;

namespace OrionDev.Infrastructure.Data.RepositoriesImpl;

public class ExpeditionRepository(AppDbContext context) : IExpeditionRepository {
  public async Task SaveAsync() {
    await context.SaveChangesAsync();
  }

  public async Task<IEnumerable<Expedition>> GetAllAsync() {
    var expeditions = await context.Expeditions.ToListAsync();
    if (expeditions == null) {
      throw new DbNotFoundException("No expeditions found");
    }

    return expeditions;
  }

  public async Task<Expedition> GetByIdAsync(Guid id) {
    var expedition = await context.Expeditions.FirstOrDefaultAsync(p => p.Id == id);
    if (expedition == null) {
      throw new DbNotFoundException("No expedition was found with this id");
    }

    return expedition;
  }

  public async Task AddAsync(Expedition expedition) {
    await context.Expeditions.AddAsync(expedition);
  }

  public async Task DeleteByIdAsync(Guid id) {
    var expedition = await context.Expeditions.FirstOrDefaultAsync(p => p.Id == id);
    if (expedition == null) {
      throw new DbNotFoundException("No expedition was found with this id");
    }

    context.Expeditions.Remove(expedition);
  }
}
