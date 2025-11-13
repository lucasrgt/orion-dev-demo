namespace OrionDev.Application.UseCases.Expedition;

public class UpdateExpedition {
}

public record UpdateExpeditionOutput(string Id, string Name);

public record UpdateExpeditionInput(string Id, string Name);
