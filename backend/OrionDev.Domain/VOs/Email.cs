using System.Text.RegularExpressions;
using OrionDev.Core.Exceptions;
using OrionDev.Core.Messages;
using OrionDev.Core.Validators;

namespace OrionDev.Domain.VOs;

public partial class Email {
  public string Value { get; private set; }

  private Email() {
  }

  public Email(string email) {
    Value = Validate(email);
  }

  private static string Validate(string email) {
    StringValidator.Validate(email, "Email", 1, 255);
    if (!EmailRegex().IsMatch(email)) {
      throw new DomainValidationException(Messages.InvalidEmail);
    }

    return email.ToLower();
  }

  [GeneratedRegex(
    """^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])$""",
    RegexOptions.IgnoreCase
  )]
  private static partial Regex EmailRegex();
}
