using System.Text.RegularExpressions;

namespace OrionDev.Core.Validators;

public static partial class HexColorValidator {
  [GeneratedRegex("^#(?:[0-9a-fA-F]{3}){1,2}$", RegexOptions.Compiled)]
  private static partial Regex HexRegex();

  public static bool IsValid(string hex) {
    return HexRegex().IsMatch(hex);
  }
}
