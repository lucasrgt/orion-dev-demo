namespace OrionDev.Application.DTOs;

public record CommentDto(
  string Id,
  DateTime CreatedOn,
  DateTime UpdatedOn,
  string Content,
  string ExpeditionId,
  string UserId,
  string NickName
);
