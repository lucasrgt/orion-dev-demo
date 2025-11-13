using OrionDev.Domain.Interfaces.Base;

namespace OrionDev.Domain.Entities;

public class Expedition : IBaseEntity {
  public Guid Id { get; private set; }
  public DateTime CreatedOn { get; private set; }
  public DateTime UpdatedOn { get; private set; }
  public string VideoPath { get; private set; }
  public List<Comment> Comments { get; private set; } = [];

  // Navigation Properties
  public Guid MissionModuleId { get; private set; }
  public MissionModule? MissionModule { get; private set; }

  public Expedition(
    Guid id,
    DateTime createdOn,
    DateTime updatedOn,
    string videoPath,
    Guid missionModuleId
  ) {
    Validate();
    Id = id;
    CreatedOn = createdOn;
    UpdatedOn = updatedOn;
    VideoPath = videoPath;
    MissionModuleId = missionModuleId;
  }

  private static void Validate() {
  }
}
