namespace OrionDev.Application.DTOs;

public record ColorPaletteDto(
  string PrimaryColorHex,
  string SecondaryColorHex,
  GradientDto Gradient
);
