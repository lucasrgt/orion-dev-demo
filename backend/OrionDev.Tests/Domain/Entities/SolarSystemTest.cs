using OrionDev.Domain.Entities;
using Shouldly;

namespace OrionDev.Tests.Domain.Entities;

public class SolarSystemTest {
  [Fact]
  public void Validate_CreateSolarSystemWithValidName_DoesNotThrowException() {
    // Arrange
    const string validName = "Name";

    // Act
    var action = () =>
      new SolarSystem(
        Guid.NewGuid(),
        DateTime.UtcNow,
        DateTime.UtcNow,
        validName
      );

    // Assert
    Should.NotThrow(action);
  }
}
