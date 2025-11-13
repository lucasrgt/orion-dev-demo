using OrionDev.Domain.Entities;
using OrionDev.Domain.Entities.Join;

namespace OrionDev.Domain.VOs;

public class MissionProgressTracker(List<UserMission> userMissions) {
  private bool HasStarted(Guid missionId) {
    return userMissions.Any(um => um.MissionId == missionId);
  }

  internal void Start(Guid userId, Mission mission) {
    if (HasStarted(mission.Id)) {
      return;
    }

    userMissions.Add(new UserMission(userId, mission.Id));
  }

  internal void Complete(Guid missionId) {
    var userMission = userMissions.FirstOrDefault(um => um.MissionId == missionId);
    userMission?.MarkAsCompleted();
  }

  internal bool IsCompleted(Guid missionId) {
    return userMissions.Any(um => um.MissionId == missionId && um.IsCompleted);
  }
}
