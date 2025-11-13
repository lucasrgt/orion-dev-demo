using Microsoft.EntityFrameworkCore;
using OrionDev.Domain.Entities;

namespace OrionDev.Infrastructure.Data.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
  // Db Sets
  // Auth
  public DbSet<User> Users { get; init; }
  public DbSet<RefreshToken> RefreshTokens { get; init; }

  // Platform
  public DbSet<SolarSystem> SolarSystems { get; init; }
  public DbSet<Planet> Planets { get; init; }
  public DbSet<Moon> Moons { get; init; }
  public DbSet<Mission> Missions { get; init; }
  public DbSet<MissionModule> MissionModules { get; init; }
  public DbSet<Expedition> Expeditions { get; init; }
  public DbSet<Comment> Comments { get; init; }


  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
  }
}
