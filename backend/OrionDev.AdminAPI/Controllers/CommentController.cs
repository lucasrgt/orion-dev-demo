using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrionDev.Application.UseCases.Comment;

namespace OrionDev.AdminAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class CommentController(
  CreateCommentOnVideo createCommentOnVideoUc,
  DeleteCommentOnVideo deleteCommentOnVideoUc,
  EditCommentOnVideo editCommentOnVideoUc
) : ControllerBase {
  [HttpPost("create-comment-on-video")]
  public async Task<IActionResult> CreateCommentOnVideo(CreateCommentOnVideoInput req) {
    var output = await createCommentOnVideoUc.Execute(req);
    return Ok(output);
  }

  [HttpDelete("delete-comment-on-video/{id:guid}")]
  public async Task<IActionResult> DeleteCommentOnVideo(Guid id) {
    var output = await deleteCommentOnVideoUc.Execute(id);
    return Ok(output);
  }

  [HttpPut("edit-comment-on-video")]
  public async Task<IActionResult> EditCommentOnVideo(EditCommentOnVideoInput req) {
    var output = await editCommentOnVideoUc.Execute(req);
    return Ok(output);
  }
}
