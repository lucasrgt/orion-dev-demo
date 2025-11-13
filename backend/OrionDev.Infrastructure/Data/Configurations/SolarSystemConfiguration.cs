using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;

namespace OrionDev.Infrastructure.Data.Configurations;

public class SolarSystemConfiguration : IEntityTypeConfiguration<SolarSystem> {
  public void Configure(EntityTypeBuilder<SolarSystem> builder) {
    builder.HasKey(s => s.Id);
    builder.Property(s => s.CreatedOn).IsRequired();
    builder.Property(s => s.UpdatedOn).IsRequired();

    builder
      .Property(s => s.Name)
      .IsRequired()
      .HasMaxLength(255);

    builder
      .HasMany(s => s.Planets)
      .WithOne(p => p.SolarSystem)
      .HasForeignKey(p => p.SolarSystemId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
