using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.MissionModule;

public class DeleteMissionModule(IMissionModuleRepository repository) {
  public async Task<DeleteMissionModuleOutput> Execute(Guid id) {
    await repository.DeleteByIdAsync(id);
    await repository.SaveAsync();
    return new DeleteMissionModuleOutput(id.ToString());
  }
}

public record DeleteMissionModuleOutput(string Id);
