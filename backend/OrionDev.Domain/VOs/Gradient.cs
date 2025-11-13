using OrionDev.Core.Exceptions;
using OrionDev.Core.Messages;
using OrionDev.Core.Utils;
using OrionDev.Core.Validators;

namespace OrionDev.Domain.VOs;

public class Gradient {
  public string StartHex { get; private set; }
  public string MiddleHex { get; private set; }
  public string EndHex { get; private set; }

  private Gradient() {
  }

  public Gradient(string startHex, string middleHex, string endHex) {
    var sanitizedStart = StringValidator.Validate(startHex, nameof(StartHex), 4, 7);
    var sanitizedMiddle = StringValidator.Validate(middleHex, nameof(MiddleHex), 4, 7);
    var sanitizedEnd = StringValidator.Validate(endHex, nameof(EndHex), 4, 7);

    ValidateHexFormat(sanitizedStart, sanitizedMiddle, sanitizedEnd);
    StartHex = HexSanitizer.SanitizeHex(sanitizedStart)!;
    MiddleHex = HexSanitizer.SanitizeHex(sanitizedMiddle)!;
    EndHex = HexSanitizer.SanitizeHex(sanitizedEnd)!;
  }


  private static void ValidateHexFormat(string start, string middle, string end) {
    if (!HexColorValidator.IsValid(start) ||
        !HexColorValidator.IsValid(middle) ||
        !HexColorValidator.IsValid(end)) {
      throw new DomainValidationException(Messages.InvalidGradientColorHex);
    }
  }
}
