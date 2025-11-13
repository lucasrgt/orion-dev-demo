using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Moon;

public class DeleteMoon(IMoonRepository repository) {
  public async Task<DeleteMoonOutput> Execute(Guid id) {
    await repository.DeleteByIdAsync(id);
    await repository.SaveAsync();

    return new DeleteMoonOutput(id.ToString());
  }
}

public record DeleteMoonOutput(string Id);
