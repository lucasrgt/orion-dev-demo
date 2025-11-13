namespace OrionDev.Domain.Entities.Join;

/// <summary>
///     Join Entity to track User Progress on Missions (complete prerequisites)
/// </summary>
public class UserMission {
  public Guid UserId { get; private set; }
  public Guid MissionId { get; private set; }
  public DateTime? CompletedOn { get; private set; }

  // Navigation Properties
  public User? User { get; private set; }
  public Mission? Mission { get; private set; }

  private UserMission() {
  }

  public UserMission(Guid playerId, Guid missionId) {
    UserId = playerId;
    MissionId = missionId;
  }

  public void MarkAsCompleted() {
    CompletedOn ??= DateTime.UtcNow;
  }

  public bool IsCompleted => CompletedOn.HasValue;
}
