using OrionDev.Application.DTOs;
using OrionDev.Domain.Entities;

namespace OrionDev.Application.Mappers;

public static class MissionModuleMapper {
  public static MissionModuleDto ToDto(MissionModule module) {
    return new MissionModuleDto(
      module.Id.ToString(),
      module.CreatedOn,
      module.UpdatedOn,
      module.Name,
      module.Expeditions.Select(ExpeditionMapper.ToDto).ToList()
    );
  }
}
