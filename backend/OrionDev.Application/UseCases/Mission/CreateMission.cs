using OrionDev.Core.Utils;
using OrionDev.Domain.Enums;
using OrionDev.Domain.Repositories;
using OrionDev.Domain.VOs;

namespace OrionDev.Application.UseCases.Mission;

public class CreateMission(IMissionRepository repository) {
  public async Task<CreateMissionOutput> Execute(CreateMissionInput input) {
    var missionDetail = new MissionDetail(
      input.ExpeditionsNumber,
      input.ExplorationTimeInMinutes,
      input.AreaCategory,
      input.Difficulty,
      input.Lore
    );

    var mission = new Domain.Entities.Mission(
      Guid.NewGuid(),
      DateTime.UtcNow,
      DateTime.UtcNow,
      input.Name,
      missionDetail,
      SlugHelper.FromName(input.Name),
      input.PlanetId is not null ? Guid.Parse(input.PlanetId) : null,
      input.MoonId is not null ? Guid.Parse(input.MoonId) : null
    );

    await repository.AddAsync(mission);
    await repository.SaveAsync();

    return new CreateMissionOutput(mission.Id.ToString());
  }
}

public record CreateMissionInput(
  string Name,
  string? PlanetId,
  string? MoonId,
  int ExpeditionsNumber,
  int ExplorationTimeInMinutes,
  AreaCategory AreaCategory,
  Difficulty Difficulty,
  string Lore
);

public record CreateMissionOutput(string Id);
