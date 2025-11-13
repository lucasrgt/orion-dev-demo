using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Moon;

public class UpdateMoon(IMoonRepository repository)
{
    public async Task<UpdateMoonOutput> Execute(UpdateMoonInput input)
    {
        var moon = await repository.GetByIdAsync(Guid.Parse(input.Id));
        if (moon == null)
        {
            throw new Exception("Lua não encontrada");
        }

        moon.UpdateName(input.Name);
        moon.UpdateLore(input.Lore);
        moon.UpdateAppearance(AppearanceMapper.FromDto(input.Appearance));
        moon.UpdateUpdatedOn(DateTime.UtcNow);

        await repository.SaveAsync();

        return new UpdateMoonOutput(MoonMapper.ToDto(moon));
    }
}

public record UpdateMoonInput(
  string Id,
  string Name,
  string Lore,
  AppearanceDto Appearance
);

public record UpdateMoonOutput(MoonDto Moon);
