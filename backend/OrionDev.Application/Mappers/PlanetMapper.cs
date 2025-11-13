using OrionDev.Application.DTOs;

namespace OrionDev.Application.Mappers;

public class PlanetMapper {
  public static PlanetDto ToDto(Domain.Entities.Planet planet) {
    var gradient = new GradientDto(
      planet.Appearance.ColorPalette.Gradient.StartHex,
      planet.Appearance.ColorPalette.Gradient.MiddleHex,
      planet.Appearance.ColorPalette.Gradient.EndHex
    );

    var colorPalette = new ColorPaletteDto(
      planet.Appearance.ColorPalette.PrimaryColorHex,
      planet.Appearance.ColorPalette.SecondaryColorHex,
      gradient
    );

    var appearance = new AppearanceDto(
      colorPalette,
      planet.Appearance.IconPath,
      planet.Appearance.BannerPath
    );

    return new PlanetDto(
      planet.Id.ToString(),
      planet.CreatedOn,
      planet.UpdatedOn,
      planet.Name,
      planet.Lore,
      appearance,
      planet.SolarSystemId.ToString()
    );
  }
}
