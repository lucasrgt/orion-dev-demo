using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;
using OrionDev.Infrastructure.Configurations.Extensions;

namespace OrionDev.Infrastructure.Data.Configurations;

public class PlanetConfiguration : IEntityTypeConfiguration<Planet> {
  public void Configure(EntityTypeBuilder<Planet> builder) {
    builder.HasKey(p => p.Id);
    builder.Property(p => p.CreatedOn).IsRequired();
    builder.Property(p => p.UpdatedOn).IsRequired();

    builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
    builder.Property(p => p.Lore).IsRequired().HasMaxLength(500);

    builder.HasMany(p => p.Moons)
      .WithOne(m => m.Planet!)
      .HasForeignKey(m => m.PlanetId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(p => p.SolarSystem)
      .WithMany(s => s.Planets)
      .HasForeignKey(p => p.SolarSystemId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.OwnsOne(p => p.Appearance, a => a.ConfigureAppearance());
  }
}
