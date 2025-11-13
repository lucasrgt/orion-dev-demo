namespace OrionDev.Application.DTOs;

public record MoonDto(
  string Id,
  DateTime CreatedOn,
  DateTime UpdatedOn,
  string Name,
  string Lore,
  AppearanceDto Appearance
);
