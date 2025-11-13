using OrionDev.Core.Exceptions;
using OrionDev.Core.Messages;
using OrionDev.Core.Validators;

namespace OrionDev.Domain.VOs;

public class Appearance {
  public ColorPalette ColorPalette { get; private set; }
  public string IconPath { get; private set; }
  public string BannerPath { get; private set; }

  private Appearance() {
  }

  public Appearance(ColorPalette colorPalette, string iconPath, string bannerPath) {
    Validate(colorPalette, iconPath, bannerPath);
    ColorPalette = colorPalette;
    IconPath = iconPath;
    BannerPath = bannerPath;
  }

  private static void Validate(ColorPalette colorPalette, string iconPath, string bannerPath) {
    if (colorPalette is null) {
      throw new DomainValidationException(Messages.ColorPaletteIsRequired);
    }

    StringValidator.Validate(iconPath, nameof(IconPath), 1, 255);
    StringValidator.Validate(bannerPath, nameof(BannerPath), 1, 255);
  }
}
