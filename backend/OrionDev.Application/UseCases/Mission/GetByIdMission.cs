using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Mission;

public class GetByIdMission(IMissionRepository repository) {
  public async Task<GetByIdMissionOutput> Execute(Guid id) {
    var mission = await repository.GetByIdAsync(id);
    var dto = MissionMapper.ToDto(mission);
    return new GetByIdMissionOutput(dto);
  }
}

public record GetByIdMissionOutput(MissionDto Mission);
