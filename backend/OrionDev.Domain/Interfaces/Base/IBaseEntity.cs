namespace OrionDev.Domain.Interfaces.Base;

public interface IBaseEntity {
  public Guid Id { get; }
  public DateTime CreatedOn { get; }
  public DateTime UpdatedOn { get; }
}
