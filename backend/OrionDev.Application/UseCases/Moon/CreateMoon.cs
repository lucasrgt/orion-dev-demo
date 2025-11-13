using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Moon;

public class CreateMoon(IMoonRepository repository) {
  public async Task<CreateMoonOutput> Execute(CreateMoonInput input) {
    var moon = new Domain.Entities.Moon(
      Guid.NewGuid(),
      DateTime.UtcNow,
      DateTime.UtcNow,
      input.Name,
      input.Lore,
      AppearanceMapper.FromDto(input.Appearance),
      Guid.Parse(input.PlanetId)
    );
    await repository.AddAsync(moon);
    await repository.SaveAsync();

    return new CreateMoonOutput(moon.Id.ToString());
  }
}

public record CreateMoonInput(
  string Name,
  string Lore,
  AppearanceDto Appearance,
  string PlanetId
);

public record CreateMoonOutput(string Id);
