using OrionDev.Domain.Entities;

namespace OrionDev.Domain.Services;

public class MissionService {
  public static bool CanAccess(User user, Mission mission) {
    return mission.IsUnlockedBy(
      user.UserMissions
        .Where(um => um.IsCompleted)
        .Select(um => um.MissionId)
        .ToList()
    );
  }

  public static void Start(User user, Mission mission) {
    user.Progress.Start(user.Id, mission);
  }

  public static void Complete(User user, Mission mission) {
    user.Progress.Complete(mission.Id);
  }
}
