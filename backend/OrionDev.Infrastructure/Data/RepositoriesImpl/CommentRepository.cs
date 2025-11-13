using Microsoft.EntityFrameworkCore;
using OrionDev.Core.Exceptions;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Repositories;
using OrionDev.Infrastructure.Data.Database;

namespace OrionDev.Infrastructure.Data.RepositoriesImpl;

public class CommentRepository(AppDbContext context) : ICommentRepository {
  public async Task SaveAsync() {
    await context.SaveChangesAsync();
  }

  public async Task<IEnumerable<Comment>> GetAllAsync() {
    var comments = await context.Comments.ToListAsync();
    if (comments == null) {
      throw new DbNotFoundException("No comments found");
    }

    return comments;
  }

  public async Task<Comment> GetByIdAsync(Guid id) {
    var comment = await context.Comments.FirstOrDefaultAsync(p => p.Id == id);
    if (comment == null) {
      throw new DbNotFoundException("No comment was found with this id");
    }

    return comment;
  }

  public async Task AddAsync(Comment comment) {
    await context.Comments.AddAsync(comment);
  }

  public async Task DeleteByIdAsync(Guid id) {
    var comment = await context.Comments.FirstOrDefaultAsync(p => p.Id == id);
    if (comment == null) {
      throw new DbNotFoundException("No comment was found with this id");
    }

    context.Comments.Remove(comment);
  }
}
