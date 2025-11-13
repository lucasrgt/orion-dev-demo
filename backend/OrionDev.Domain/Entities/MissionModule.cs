using OrionDev.Domain.Interfaces.Base;

namespace OrionDev.Domain.Entities;

public class MissionModule : IBaseEntity {
  public Guid Id { get; private set; }
  public DateTime CreatedOn { get; private set; }
  public DateTime UpdatedOn { get; private set; }
  public string Name { get; private set; }
  public List<Expedition> Expeditions { get; private set; } = [];

  private MissionModule() {
  }

  public MissionModule(
    Guid id,
    DateTime createdOn,
    DateTime updatedOn,
    string name
  ) {
    Id = id;
    CreatedOn = createdOn;
    UpdatedOn = updatedOn;
    Name = name;
  }
}
