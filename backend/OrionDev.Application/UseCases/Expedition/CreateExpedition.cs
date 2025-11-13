using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Expedition;

public class CreateExpedition(IExpeditionRepository repository) {
  public async Task<CreateExpeditionOutput> Execute(CreateExpeditionInput input) {
    var expedition = new Domain.Entities.Expedition(
      Guid.NewGuid(),
      DateTime.UtcNow,
      DateTime.UtcNow,
      input.VideoPath,
      Guid.Parse(input.MissionModuleId)
    );

    await repository.AddAsync(expedition);
    await repository.SaveAsync();

    return new CreateExpeditionOutput(expedition.Id.ToString());
  }
}

public record CreateExpeditionInput(
  string Name,
  string VideoPath,
  string MissionModuleId
);

public record CreateExpeditionOutput(string Id);
