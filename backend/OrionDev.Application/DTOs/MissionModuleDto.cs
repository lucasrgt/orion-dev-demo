namespace OrionDev.Application.DTOs;

public record MissionModuleDto(
  string Id,
  DateTime CreatedOn,
  DateTime UpdatedOn,
  string Name,
  List<ExpeditionDto> Expeditions
);
