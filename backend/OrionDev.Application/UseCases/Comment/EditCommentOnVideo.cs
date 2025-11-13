using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Comment;

public class EditCommentOnVideo(ICommentRepository repository) {
  public async Task<EditCommentOnVideoOutput> Execute(EditCommentOnVideoInput input) {
    var comment = await repository.GetByIdAsync(Guid.Parse(input.Id));

    comment.EditContent(input.NewContent);

    await repository.SaveAsync();

    return new EditCommentOnVideoOutput(comment.Id.ToString());
  }
}

public record EditCommentOnVideoInput(
  string Id,
  string NewContent
);

public record EditCommentOnVideoOutput(string Id);
