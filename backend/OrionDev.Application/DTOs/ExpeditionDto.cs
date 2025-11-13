namespace OrionDev.Application.DTOs;

public class ExpeditionDto(
  string Id,
  DateTime CreatedOn,
  DateTime UpdatedOn,
  string VideoPath,
  List<CommentDto> Comments
);
