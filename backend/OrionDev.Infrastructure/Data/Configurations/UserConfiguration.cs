using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;

namespace OrionDev.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User> {
  public void Configure(EntityTypeBuilder<User> builder) {
    builder.HasKey(u => u.Id);

    builder.Property(u => u.CreatedOn).IsRequired();
    builder.Property(u => u.UpdatedOn).IsRequired();

    builder.OwnsOne(u => u.Email, email => {
      email.Property(e => e.Value)
        .IsRequired()
        .HasMaxLength(255)
        .HasColumnName("Email");
    });

    builder.OwnsOne(u => u.Password, password => {
      password.Property(p => p.Value)
        .IsRequired()
        .HasMaxLength(255)
        .HasColumnName("PasswordHash");
    });

    builder.Property(u => u.FullName)
      .IsRequired()
      .HasMaxLength(255);

    builder.Property(u => u.Role)
      .IsRequired()
      .HasConversion<string>()
      .HasMaxLength(20);
  }
}
