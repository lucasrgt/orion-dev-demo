using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Mission;

public class GetAllMissions(IMissionRepository repository) {
  public async Task<GetAllMissionOutput> Execute() {
    var missions = await repository.GetAllAsync();
    var missionDtos = missions.Select(MissionMapper.ToDto).ToList();
    return new GetAllMissionOutput(missionDtos);
  }
}

public record GetAllMissionOutput(List<MissionDto> Mission);
