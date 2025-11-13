using OrionDev.Core.Exceptions;
using OrionDev.Core.Validators;
using Shouldly;

namespace OrionDev.Tests.Core.Validators;

public class StringValidatorTest {
  [Fact]
  public void Validate_ValidString_DoesNotThrowException() {
    // Arrange
    const string? validString = "valid string";

    // Act
    var action = () => StringValidator.Validate(validString, "TestString", 12, 12);

    // Assert
    Should.NotThrow(action);
  }

  [Fact]
  public void Validate_EmptyString_ThrowsStringValidationException() {
    // Arrange
    const string? emptyString = "";

    // Act
    var exception = Should.Throw<StringValidationException>(() =>
      StringValidator.Validate(emptyString, "TestString", 8, 12));

    // Assert
    exception.Message.ShouldBe("Invalid TestString. TestString is required.");
  }

  [Fact]
  public void Validate_WhiteSpaceString_ThrowsStringValidationException() {
    // Arrange
    const string? whiteSpaceString = " ";

    // Act
    var exception = Should.Throw<StringValidationException>(
      () => StringValidator.Validate(whiteSpaceString, "TestString", 8, 12)
    );

    // Assert
    exception.Message.ShouldBe("Invalid TestString. TestString is required.");
  }

  [Fact]
  public void Validate_PreWhiteSpaceString_DoesNotThrowException() {
    // Arrange
    const string? preWhiteSpaceString = " name";

    // Act
    var action = () => StringValidator.Validate(preWhiteSpaceString, "TestString", 1, 12);

    // Assert
    Should.NotThrow(action);
  }

  [Fact]
  public void Validate_ShortString_ThrowsStringValidationException() {
    // Arrange
    const string? shortString = "invalid";

    // Act
    var exception = Should.Throw<StringValidationException>(
      () => StringValidator.Validate(shortString, "TestString", 8, 12)
    );

    // Assert
    exception.Message.ShouldBe("Invalid TestString. TestString must have at least 8 characters.");
  }

  [Fact]
  public void Validate_LongString_ThrowsStringValidationException() {
    // Arrange
    var longString = new string('a', 13);

    // Act
    var exception = Should.Throw<StringValidationException>(
      () => StringValidator.Validate(longString, "TestString", 8, 12)
    );

    // Assert
    exception.Message.ShouldBe("Invalid TestString. TestString must have 12 characters or less.");
  }
}
