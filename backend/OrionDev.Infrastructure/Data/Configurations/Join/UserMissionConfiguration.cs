using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionDev.Domain.Entities.Join;

namespace OrionDev.Infrastructure.Configurations.Join;

public class UserMissionConfiguration : IEntityTypeConfiguration<UserMission> {
  public void Configure(EntityTypeBuilder<UserMission> builder) {
    builder.HasKey(um => new { um.UserId, um.MissionId });

    builder
      .Property(um => um.CompletedOn);

    builder
      .HasOne(um => um.Mission)
      .WithMany(m => m.UserMissions)
      .HasForeignKey(um => um.MissionId)
      .OnDelete(DeleteBehavior.Restrict);

    builder
      .HasOne(um => um.User)
      .WithMany(u => u.UserMissions)
      .HasForeignKey(um => um.UserId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
 
