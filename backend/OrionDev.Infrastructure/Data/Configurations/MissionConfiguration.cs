using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;

namespace OrionDev.Infrastructure.Data.Configurations;

public class MissionConfiguration : IEntityTypeConfiguration<Mission> {
  public void Configure(EntityTypeBuilder<Mission> builder) {
    builder.HasKey(m => m.Id);
    builder.Property(m => m.CreatedOn).IsRequired();
    builder.Property(m => m.UpdatedOn).IsRequired();

    builder.Property(m => m.Slug)
      .IsRequired()
      .HasMaxLength(100);

    builder.HasIndex(m => m.Slug)
      .IsUnique();

    builder.HasMany(m => m.PrerequisiteMissions)
      .WithMany()
      .UsingEntity(j => j.ToTable("MissionPrerequisites"));

    builder.HasOne(m => m.Planet)
      .WithMany()
      .HasForeignKey(m => m.PlanetId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasOne(m => m.Moon)
      .WithOne(moon => moon.Mission)
      .HasForeignKey<Mission>(m => m.MoonId)
      .OnDelete(DeleteBehavior.Restrict);

    builder.OwnsOne(m => m.Detail, detail => {
      detail.Property(d => d.ExpeditionsNumber)
        .IsRequired();

      detail.Property(d => d.ExplorationTimeInMinutes)
        .IsRequired();

      detail.Property(d => d.Lore)
        .IsRequired()
        .HasMaxLength(1000);

      detail.Property(d => d.AreaCategory)
        .HasConversion<int>()
        .IsRequired();

      detail.Property(d => d.Difficulty)
        .HasConversion<int>()
        .IsRequired();
    });
  }
}
