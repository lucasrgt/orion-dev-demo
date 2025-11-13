using OrionDev.Application.DTOs;
using OrionDev.Domain.VOs;

namespace OrionDev.Application.Mappers;

public class AppearanceMapper {
  public static AppearanceDto ToDto(Appearance appearance) {
    var gradient = new GradientDto(
      appearance.ColorPalette.Gradient.StartHex,
      appearance.ColorPalette.Gradient.MiddleHex,
      appearance.ColorPalette.Gradient.EndHex
    );

    var colorPalette = new ColorPaletteDto(
      appearance.ColorPalette.Gradient.StartHex,
      appearance.ColorPalette.SecondaryColorHex,
      gradient
    );

    return new AppearanceDto(
      colorPalette,
      appearance.IconPath,
      appearance.BannerPath
    );
  }

  public static Appearance FromDto(AppearanceDto appearanceDto) {
    var gradient = new Gradient(
      appearanceDto.ColorPalette.Gradient.StartHex,
      appearanceDto.ColorPalette.Gradient.MiddleHex,
      appearanceDto.ColorPalette.Gradient.EndHex
    );

    var colorPalette = new ColorPalette(
      appearanceDto.ColorPalette.Gradient.StartHex,
      appearanceDto.ColorPalette.SecondaryColorHex,
      gradient
    );

    return new Appearance(
      colorPalette,
      appearanceDto.IconPath,
      appearanceDto.BannerPath
    );
  }
}
