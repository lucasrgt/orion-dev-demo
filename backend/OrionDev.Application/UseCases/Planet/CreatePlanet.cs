using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Planet;

public class CreatePlanet(IPlanetRepository repository) {
  public async Task<CreatePlanetOutput> Execute(CreatePlanetInput input) {
    var planet = new Domain.Entities.Planet(
      Guid.NewGuid(),
      DateTime.UtcNow,
      DateTime.UtcNow,
      input.Name,
      input.Lore,
      AppearanceMapper.FromDto(input.Appearance),
      Guid.Parse(input.SolarSystemId)
    );
    await repository.AddAsync(planet);
    await repository.SaveAsync();

    return new CreatePlanetOutput(planet.Id.ToString());
  }
}

public record CreatePlanetInput(
  string Name,
  string Lore,
  AppearanceDto Appearance,
  string SolarSystemId);

public record CreatePlanetOutput(string Id);
