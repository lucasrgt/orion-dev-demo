using OrionDev.Core.Validators;

namespace OrionDev.Core.Utils;

public static class HexSanitizer {
  public static List<string> SanitizeHexList(IEnumerable<string> input) {
    return input
      .Where(c => !string.IsNullOrWhiteSpace(c))
      .Select(c => c.Trim())
      .Where(HexColorValidator.IsValid)
      .Distinct()
      .ToList();
  }

  public static string? SanitizeHex(string? input) {
    if (string.IsNullOrWhiteSpace(input)) {
      return null;
    }

    var trimmed = input.Trim();

    return HexColorValidator.IsValid(trimmed) ? trimmed : null;
  }
}
