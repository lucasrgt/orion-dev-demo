using OrionDev.Core.Validators;
using OrionDev.Domain.Interfaces.Base;
using OrionDev.Domain.VOs;

namespace OrionDev.Domain.Entities;

/// <summary>
///     Planet equals to a Formation with a Main Course
/// </summary>
public class Planet : IBaseEntity {
  public Guid Id { get; private set; }
  public DateTime CreatedOn { get; private set; }
  public DateTime UpdatedOn { get; private set; }
  public string Name { get; private set; }
  public string Lore { get; private set; }
  public Appearance Appearance { get; private set; }
  public List<Moon> Moons { get; private set; } = [];

  // Navigation Properties
  public Guid SolarSystemId { get; private set; }
  public SolarSystem? SolarSystem { get; private set; }

  private Planet() {
  }

  public Planet(
    Guid id,
    DateTime createdOn,
    DateTime updatedOn,
    string name,
    string lore,
    Appearance appearance,
    Guid solarSystemId
  ) {
    Validate(name, lore);
    Name = name;
    Lore = lore;
    Appearance = appearance;
    Id = id;
    CreatedOn = createdOn;
    UpdatedOn = updatedOn;
    SolarSystemId = solarSystemId;
  }

  private static void Validate(string name, string lore) {
    StringValidator.Validate(name, "Name", 1, 255);
    StringValidator.Validate(lore, "Lore", 1, 500);
  }
}
