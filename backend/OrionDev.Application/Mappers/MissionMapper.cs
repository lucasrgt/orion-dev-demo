using OrionDev.Application.DTOs;
using OrionDev.Domain.Entities;
using OrionDev.Domain.VOs;

namespace OrionDev.Application.Mappers;

public static class MissionMapper {
  public static MissionDto ToDto(Mission mission) {
    var detail = new MissionDetailDto(
      mission.Detail.ExpeditionsNumber,
      mission.Detail.ExplorationTimeInMinutes,
      mission.Detail.AreaCategory,
      mission.Detail.Difficulty,
      mission.Detail.Lore
    );

    return new MissionDto(
      mission.Id.ToString(),
      mission.CreatedOn,
      mission.UpdatedOn,
      mission.Name,
      detail,
      mission.Slug,
      mission.PlanetId?.ToString(),
      mission.MoonId?.ToString(),
      mission.PrerequisiteMissions.Select(p =>
        new PrerequisiteMissionDto(p.Id.ToString(), p.Name)
      ).ToList()
    );
  }

  public static Mission FromDto(MissionDto missionDto) {
    return new Mission(
      Guid.NewGuid(),
      DateTime.UtcNow,
      DateTime.UtcNow,
      missionDto.Name,
      new MissionDetail(
        missionDto.Detail.ExpeditionsNumber,
        missionDto.Detail.ExplorationTimeInMinutes,
        missionDto.Detail.AreaCategory,
        missionDto.Detail.Difficulty,
        missionDto.Detail.Lore
      ),
      missionDto.Slug,
      string.IsNullOrWhiteSpace(missionDto.PlanetId) ? null : Guid.Parse(missionDto.PlanetId),
      string.IsNullOrWhiteSpace(missionDto.MoonId) ? null : Guid.Parse(missionDto.MoonId)
    );
  }
}
