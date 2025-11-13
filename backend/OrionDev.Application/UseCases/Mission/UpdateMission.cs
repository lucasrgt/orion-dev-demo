using OrionDev.Domain.Enums;
using OrionDev.Domain.Repositories;
using OrionDev.Domain.VOs;

namespace OrionDev.Application.UseCases.Mission;

public class UpdateMission(IMissionRepository repository)
{
    public async Task<UpdateMissionOutput> Execute(UpdateMissionInput input)
    {
        var mission = await repository.GetByIdAsync(Guid.Parse(input.Id));
        if (mission == null)
        {
            throw new Exception("Missão não encontrada");
        }

        var missionDetail = new MissionDetail(
          input.ExpeditionsNumber,
          input.ExplorationTimeInMinutes,
          input.AreaCategory,
          input.Difficulty,
          input.Lore
        );

        mission.UpdateName(input.Name);
        mission.UpdateDetail(missionDetail);
        mission.UpdateUpdatedOn(DateTime.UtcNow);

        await repository.SaveAsync();

        return new UpdateMissionOutput(mission.Id.ToString(), mission.Name);
    }
}

public record UpdateMissionInput(
  string Id,
  string Name,
  int ExpeditionsNumber,
  int ExplorationTimeInMinutes,
  AreaCategory AreaCategory,
  Difficulty Difficulty,
  string Lore
);

public record UpdateMissionOutput(string Id, string Name);
