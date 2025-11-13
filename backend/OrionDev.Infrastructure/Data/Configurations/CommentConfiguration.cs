using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;

namespace OrionDev.Infrastructure.Data.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment> {
  public void Configure(EntityTypeBuilder<Comment> builder) {
    builder.HasKey(c => c.Id);
    builder.Property(c => c.CreatedOn).IsRequired();
    builder.Property(c => c.UpdatedOn).IsRequired();

    builder.Property(c => c.Content)
      .IsRequired()
      .HasMaxLength(500);

    builder.HasOne(c => c.User)
      .WithMany(u => u.Comments)
      .HasForeignKey(c => c.UserId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(c => c.Expedition)
      .WithMany(e => e.Comments)
      .HasForeignKey(c => c.ExpeditionId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.Property(c => c.UserId).IsRequired();
    builder.Property(c => c.ExpeditionId).IsRequired();
  }
}
