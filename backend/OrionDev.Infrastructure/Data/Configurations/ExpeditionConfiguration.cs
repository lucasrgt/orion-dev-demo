using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities;

namespace OrionDev.Infrastructure.Data.Configurations;

public class ExpeditionConfiguration : IEntityTypeConfiguration<Expedition> {
  public void Configure(EntityTypeBuilder<Expedition> builder) {
    builder.HasKey(e => e.Id);

    builder.Property(e => e.CreatedOn).IsRequired();
    builder.Property(e => e.UpdatedOn).IsRequired();
    builder.Property(e => e.VideoPath).IsRequired().HasMaxLength(255);

    builder.HasMany(e => e.Comments)
      .WithOne(c => c.Expedition!)
      .HasForeignKey(c => c.ExpeditionId)
      .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(e => e.MissionModule)
      .WithMany(m => m.Expeditions)
      .HasForeignKey(e => e.MissionModuleId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
