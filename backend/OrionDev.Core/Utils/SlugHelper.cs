using System.Globalization;
using System.Text;

namespace OrionDev.Core.Utils;

public static class SlugHelper {
  public static string FromName(string name) {
    return name
      .Trim()
      .ToLowerInvariant()
      .Replace(" ", "-")
      .Replace("--", "-")
      .Replace(".", "")
      .Normalize(NormalizationForm.FormD)
      .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
      .Aggregate("", (s, c) => s + c)
      .Replace("--", "-")
      .Replace("---", "-");
  }
}
