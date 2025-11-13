using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;

namespace OrionDev.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken> {
  public void Configure(EntityTypeBuilder<RefreshToken> builder) {
    builder.HasKey(rt => rt.Id);

    builder.Property(rt => rt.Token)
      .IsRequired()
      .HasMaxLength(2048);

    builder.Property(rt => rt.Expires)
      .IsRequired();

    builder.Property(rt => rt.UserId)
      .IsRequired();

    builder.HasIndex(rt => rt.Token)
      .IsUnique();
  }
}
