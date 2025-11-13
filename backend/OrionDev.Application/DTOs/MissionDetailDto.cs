using OrionDev.Domain.Enums;

namespace OrionDev.Application.DTOs;

public record MissionDetailDto(
  int ExpeditionsNumber,
  int ExplorationTimeInMinutes,
  AreaCategory AreaCategory,
  Difficulty Difficulty,
  string Lore
);
