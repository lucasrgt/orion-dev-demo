namespace OrionDev.Application.DTOs;

public record MissionDto(
  string Id,
  DateTime CreatedOn,
  DateTime UpdatedOn,
  string Name,
  MissionDetailDto Detail,
  string Slug,
  string? PlanetId,
  string? MoonId,
  List<PrerequisiteMissionDto> PrerequisiteMissions
);
