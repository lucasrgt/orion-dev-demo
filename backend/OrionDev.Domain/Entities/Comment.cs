using OrionDev.Core.Validators;
using OrionDev.Domain.Interfaces.Base;

namespace OrionDev.Domain.Entities;

public class Comment : IBaseEntity {
  public Guid Id { get; private set; }
  public DateTime CreatedOn { get; private set; }
  public DateTime UpdatedOn { get; private set; }
  public string Content { get; private set; }

  // Navigation properties
  public Guid UserId { get; private set; }
  public User? User { get; private set; }
  public Guid ExpeditionId { get; private set; }
  public Expedition? Expedition { get; private set; }

  private Comment() {
  }

  public Comment(
    Guid id,
    DateTime createdOn,
    DateTime updatedOn,
    string content,
    Guid userId,
    Guid expeditionId
  ) {
    Validate(content);

    Id = id;
    CreatedOn = createdOn;
    UpdatedOn = updatedOn;
    Content = content;
    UserId = userId;
    ExpeditionId = expeditionId;
  }

  private static void Validate(string content) {
    StringValidator.Validate(content, nameof(Content), 1, 1000);
  }

  public void EditContent(string newContent) {
    Validate(newContent);
    Content = newContent;
    UpdatedOn = DateTime.UtcNow;
  }
}
