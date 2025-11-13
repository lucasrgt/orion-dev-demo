using OrionDev.Application.DTOs;
using OrionDev.Domain.Entities;

namespace OrionDev.Application.Mappers;

public class MoonMapper {
  public static MoonDto ToDto(Moon moon) {
    var gradient = new GradientDto(
      moon.Appearance.ColorPalette.Gradient.StartHex,
      moon.Appearance.ColorPalette.Gradient.MiddleHex,
      moon.Appearance.ColorPalette.Gradient.EndHex
    );

    var colorPalette = new ColorPaletteDto(
      moon.Appearance.ColorPalette.PrimaryColorHex,
      moon.Appearance.ColorPalette.SecondaryColorHex,
      gradient
    );

    var appearance = new AppearanceDto(
      colorPalette,
      moon.Appearance.IconPath,
      moon.Appearance.BannerPath
    );

    return new MoonDto(
      moon.Id.ToString(),
      moon.CreatedOn,
      moon.UpdatedOn,
      moon.Name,
      moon.Lore,
      appearance
    );
  }
}
