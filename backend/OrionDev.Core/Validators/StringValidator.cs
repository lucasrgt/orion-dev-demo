using System.Text.RegularExpressions;
using OrionDev.Core.Exceptions;

namespace OrionDev.Core.Validators;

public static partial class StringValidator {
  public static string Validate(
    string value,
    string paramName,
    int minLength,
    int maxLength
  ) {
    var sanitizedValue = value.Trim();
    StringValidationException.When(
      string.IsNullOrEmpty(sanitizedValue),
      $"Invalid {paramName}. {paramName} is required."
    );
    StringValidationException.When(
      string.IsNullOrWhiteSpace(sanitizedValue),
      $"Invalid {paramName}. {paramName} is required."
    );
    StringValidationException.When(
      sanitizedValue.Length < minLength,
      $"Invalid {paramName}. {paramName} must have at least {minLength} characters."
    );
    StringValidationException.When(
      sanitizedValue.Length > maxLength,
      $"Invalid {paramName}. {paramName} must have {maxLength} characters or less."
    );

    return sanitizedValue;
  }

  public static string ValidateSlug(string value,
    string paramName = "Slug",
    int minLength = 3,
    int maxLength = 100
  ) {
    var sanitized = Validate(value, paramName, minLength, maxLength);

    var slugRegex = SlugRegex();

    StringValidationException.When(!slugRegex.IsMatch(sanitized),
      $"Invalid {paramName}. It must be lowercase, use only letters, numbers and hyphens, and follow kebab-case.");

    return sanitized;
  }

  [GeneratedRegex("^[a-z0-9]+(?:-[a-z0-9]+)*$")]
  private static partial Regex SlugRegex();
}
