using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;
using OrionDev.Infrastructure.Configurations.Extensions;

namespace OrionDev.Infrastructure.Data.Configurations;

public class MoonConfiguration : IEntityTypeConfiguration<Moon> {
  public void Configure(EntityTypeBuilder<Moon> builder) {
    builder.HasKey(m => m.Id);
    builder.Property(m => m.CreatedOn).IsRequired();
    builder.Property(m => m.UpdatedOn).IsRequired();

    builder.Property(m => m.Name).IsRequired().HasMaxLength(255);
    builder.Property(m => m.Lore).IsRequired().HasMaxLength(500);

    builder.HasOne(m => m.Planet)
      .WithMany(p => p.Moons)
      .HasForeignKey(m => m.PlanetId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.OwnsOne(m => m.Appearance, a => a.ConfigureAppearance());
  }
}
