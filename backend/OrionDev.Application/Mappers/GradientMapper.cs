using OrionDev.Application.DTOs;
using OrionDev.Domain.VOs;

namespace OrionDev.Application.Mappers;

public class GradientMapper {
  public static GradientDto ToDto(Gradient gradient) {
    return new GradientDto(
      gradient.StartHex,
      gradient.MiddleHex,
      gradient.EndHex
    );
  }
}
