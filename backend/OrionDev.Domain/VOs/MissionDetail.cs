using OrionDev.Domain.Enums;

namespace OrionDev.Domain.VOs;

public class MissionDetail {
  public int ExpeditionsNumber { get; private set; }
  public int ExplorationTimeInMinutes { get; private set; }
  public AreaCategory AreaCategory { get; private set; }
  public Difficulty Difficulty { get; private set; }
  public string Lore { get; private set; }

  public MissionDetail(
    int expeditionsNumber,
    int explorationTimeInMinutes,
    AreaCategory areaCategory,
    Difficulty difficulty,
    string lore
  ) {
    Validate();
    ExpeditionsNumber = expeditionsNumber;
    ExplorationTimeInMinutes = explorationTimeInMinutes;
    AreaCategory = areaCategory;
    Difficulty = difficulty;
    Lore = lore;
  }

  private static void Validate() {
  }
}
