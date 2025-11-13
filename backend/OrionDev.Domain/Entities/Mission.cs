using OrionDev.Core.Exceptions;
using OrionDev.Core.Messages;
using OrionDev.Core.Validators;
using OrionDev.Domain.Entities.Join;
using OrionDev.Domain.Enums;
using OrionDev.Domain.Interfaces.Base;
using OrionDev.Domain.VOs;

namespace OrionDev.Domain.Entities;

public class Mission : IBaseEntity {
  public Guid Id { get; private set; }
  public DateTime CreatedOn { get; private set; }
  public DateTime UpdatedOn { get; private set; }
  public string Name { get; private set; }
  public MissionDetail Detail { get; private set; }
  public List<Mission> PrerequisiteMissions { get; private set; } = [];
  public List<MissionModule> MissionModules { get; private set; } = [];
  public List<UserMission> UserMissions { get; private set; } = [];
  public string Slug { get; private set; }


  // Navigation Properties
  public Guid? PlanetId { get; private set; }
  public Planet? Planet { get; private set; }
  public Guid? MoonId { get; private set; }
  public Moon? Moon { get; private set; }

  public MissionType Type =>
    PlanetId is not null ? MissionType.Planet : MissionType.Moon;


  private Mission() {
  }

  public Mission(
    Guid id,
    DateTime createdOn,
    DateTime updatedOn,
    string name,
    MissionDetail detail,
    string slug,
    Guid? planetId = null,
    Guid? moonId = null,
    List<Mission>? prerequisiteMissions = null,
    List<MissionModule>? missionModules = null
  ) {
    Validate(name, slug, planetId, moonId);
    Id = id;
    CreatedOn = createdOn;
    UpdatedOn = updatedOn;
    Name = name;
    Detail = detail;
    Slug = slug;
    PlanetId = planetId;
    MoonId = moonId;

    if (prerequisiteMissions is not null) {
      PrerequisiteMissions = prerequisiteMissions;
    }

    if (missionModules is not null) {
      MissionModules = missionModules;
    }
  }

  private static void Validate(string name, string slug, Guid? planetId, Guid? moonId) {
    StringValidator.Validate(name, nameof(Name), 1, 255);
    StringValidator.ValidateSlug(slug);

    if (planetId != null && moonId != null) {
      throw new DomainValidationException(Messages.MissionCannotHavePlanetAndMoon);
    }

    if (planetId is null && moonId is null) {
      throw new DomainValidationException(Messages.MissionMustHavePlanetOrMoon);
    }
  }

  public bool IsUnlockedBy(List<Guid> completedMissionIds) {
    return PrerequisiteMissions.All(p => completedMissionIds.Contains(p.Id));
  }
}
