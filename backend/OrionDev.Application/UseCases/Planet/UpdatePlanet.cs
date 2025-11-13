using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Planet;

public class UpdatePlanet(IPlanetRepository repository)
{
    public async Task<UpdatePlanetOutput> Execute(UpdatePlanetInput input)
    {
        var planet = await repository.GetByIdAsync(Guid.Parse(input.Id));
        if (planet == null)
        {
            throw new Exception("Planeta não encontrado");
        }

        planet.UpdateName(input.Name);
        planet.UpdateLore(input.Lore);
        planet.UpdateAppearance(AppearanceMapper.FromDto(input.Appearance));
        planet.UpdateUpdatedOn(DateTime.UtcNow);

        await repository.SaveAsync();

        return new UpdatePlanetOutput(PlanetMapper.ToDto(planet));
    }
}

public record UpdatePlanetInput(
  string Id,
  string Name,
  string Lore,
  AppearanceDto Appearance
);

public record UpdatePlanetOutput(PlanetDto Planet);
