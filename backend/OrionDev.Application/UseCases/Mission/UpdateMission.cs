namespace OrionDev.Application.UseCases.Mission;

public class UpdateMission {
}

public record UpdateMissionOutput(string Id, string Name);

public record UpdateMissionInput(string Id, string Name);
