using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;

namespace OrionDev.Infrastructure.Data.Configurations;

public class MissionModuleConfiguration : IEntityTypeConfiguration<MissionModule> {
  public void Configure(EntityTypeBuilder<MissionModule> builder) {
    builder.HasKey(m => m.Id);

    builder.Property(m => m.CreatedOn).IsRequired();
    builder.Property(m => m.UpdatedOn).IsRequired();
    builder.Property(m => m.Name).IsRequired().HasMaxLength(255);

    builder.HasMany(m => m.Expeditions)
      .WithOne(e => e.MissionModule!)
      .HasForeignKey(e => e.MissionModuleId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
