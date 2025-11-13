using OrionDev.Core.Exceptions;
using OrionDev.Core.Messages;
using OrionDev.Core.Validators;
using OrionDev.Domain.VOs;

public class ColorPalette {
  public string PrimaryColorHex { get; private set; }
  public string SecondaryColorHex { get; private set; }
  public Gradient Gradient { get; private set; }

  private ColorPalette() {
  }

  public ColorPalette(string primaryColor, string secondaryColor, Gradient gradient) {
    Validate(primaryColor, secondaryColor, gradient);

    PrimaryColorHex = primaryColor;
    SecondaryColorHex = secondaryColor;
    Gradient = gradient;
  }

  private static void Validate(string primaryColor, string secondaryColor, Gradient gradient) {
    if (!HexColorValidator.IsValid(primaryColor)) {
      throw new DomainValidationException(Messages.InvalidPrimaryColorHex);
    }

    if (!HexColorValidator.IsValid(secondaryColor)) {
      throw new DomainValidationException(Messages.InvalidSecondaryColorHex);
    }

    if (gradient is null) {
      throw new DomainValidationException(Messages.EmptyPrimaryGradient);
    }
  }
}
