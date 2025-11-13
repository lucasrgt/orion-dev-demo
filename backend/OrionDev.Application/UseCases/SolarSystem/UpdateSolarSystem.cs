namespace OrionDev.Application.UseCases.SolarSystem;

public class UpdateSolarSystem {
}

public record UpdateSolarSystemOutput(string Id, string Name);

public record UpdateSolarSystemInput(string Id, string Name);
