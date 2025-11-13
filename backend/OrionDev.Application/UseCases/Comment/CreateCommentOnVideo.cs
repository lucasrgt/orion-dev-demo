using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Comment;

public class CreateCommentOnVideo(ICommentRepository repository) {
  public async Task<CreateCommentOnVideoOutput> Execute(CreateCommentOnVideoInput input) {
    var comment = new Domain.Entities.Comment(
      Guid.NewGuid(),
      DateTime.UtcNow,
      DateTime.UtcNow,
      input.Content,
      Guid.Parse(input.UserId),
      Guid.Parse(input.ExpeditionId)
    );

    await repository.AddAsync(comment);
    await repository.SaveAsync();

    return new CreateCommentOnVideoOutput(comment.Id.ToString());
  }
}

public record CreateCommentOnVideoInput(
  string Content,
  string UserId,
  string ExpeditionId
);

public record CreateCommentOnVideoOutput(string Id);
