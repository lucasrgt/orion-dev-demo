namespace OrionDev.Application.DTOs;

public record PlanetDto(
  string Id,
  DateTime CreatedOn,
  DateTime UpdatedOn,
  string Name,
  string Lore,
  AppearanceDto Appearance,
  string SolarSystemId
);
