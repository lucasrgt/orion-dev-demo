using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.MissionModule;

public class GetAllMissionModules(IMissionModuleRepository repository) {
  public async Task<GetAllMissionModulesOutput> Execute() {
    var missionsModules = await repository.GetAllAsync();
    var missionModuleDtos = missionsModules.Select(MissionModuleMapper.ToDto).ToList();
    return new GetAllMissionModulesOutput(missionModuleDtos);
  }
}

public record GetAllMissionModulesOutput(List<MissionModuleDto> MissionModules);
