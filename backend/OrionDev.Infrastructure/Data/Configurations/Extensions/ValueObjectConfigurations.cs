using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.VOs;

namespace OrionDev.Infrastructure.Configurations.Extensions;

public static class ValueObjectConfigurations {
  public static void ConfigureAppearance<T>(this OwnedNavigationBuilder<T, Appearance> builder)
    where T : class {
    builder.Property(a => a.IconPath).IsRequired().HasMaxLength(255);
    builder.Property(a => a.BannerPath).IsRequired().HasMaxLength(255);

    builder.OwnsOne(a => a.ColorPalette, b => b.ConfigureColorPalette());
  }

  public static void ConfigureColorPalette<T>(this OwnedNavigationBuilder<T, ColorPalette> builder)
    where T : class {
    builder.Property(c => c.PrimaryColorHex).IsRequired().HasMaxLength(7);
    builder.Property(c => c.SecondaryColorHex).IsRequired().HasMaxLength(7);

    builder.OwnsOne(c => c.Gradient, g => g.ConfigureGradient());
  }

  public static void ConfigureGradient<T>(this OwnedNavigationBuilder<T, Gradient> builder)
    where T : class {
    builder.Property(g => g.StartHex).IsRequired().HasMaxLength(7);
    builder.Property(g => g.MiddleHex).IsRequired().HasMaxLength(7);
    builder.Property(g => g.EndHex).IsRequired().HasMaxLength(7);
  }
}
