using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrionDev.Domain.Entities;
using OrionDev.Domain.Interfaces;
using OrionDev.Infrastructure.Helpers;

namespace OrionDev.Infrastructure.Data.Database;

public static class PrepDb {
  public static void PrepPopulation(IApplicationBuilder app) {
    using var serviceScope = app.ApplicationServices.CreateScope();
    using var db = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    var encryptionService =
      serviceScope.ServiceProvider.GetRequiredService<IPasswordEncryptionService>();


    if (db.Database.GetPendingMigrations().Any()) {
      db.Database.Migrate();
      Console.WriteLine("--> Applying Migrations ...");
    } else {
      Console.WriteLine("--> No Migrations are pending. Proceeding with seeding...");
    }

    SeedData(db, encryptionService);
  }


  private static void SeedData(AppDbContext context, IPasswordEncryptionService encryptionService) {
    SeedAdminUsers(context, encryptionService);

    if (!context.SolarSystems.Any()) {
      SeedSolarSystems(context);
      return;
    }

    Console.WriteLine("--> We already have SolarSystem data");
  }

  private static void SeedSolarSystems(AppDbContext context) {
    Console.WriteLine("--> Seeding SolarSystems...");

    var solarSystems = new List<SolarSystem> {
      new(
        Guid.NewGuid(),
        name: "Pleaides",
        createdOn: DateTime.UtcNow,
        updatedOn: DateTime.UtcNow
      ),
      new(
        Guid.NewGuid(),
        name: "Orion",
        createdOn: DateTime.UtcNow,
        updatedOn: DateTime.UtcNow
      )
    };

    context.SolarSystems.AddRange(solarSystems);
    context.SaveChanges();
    Console.WriteLine($"--> Seeded {solarSystems.Count}");
  }

  // ADMIN-ONLY
  // PASSWORD: Admin@123
  private static void SeedAdminUsers(
    AppDbContext context,
    IPasswordEncryptionService encryptionService
  ) {
    var existingAdmins = context.Users
      .Where(u =>
        u.Email.Value == "lucas@oriondev.com.br")
      .ToList();

    if (existingAdmins.Count == 2) {
      Console.WriteLine("--> Admin users already exist.");
      return;
    }

    Console.WriteLine("--> Seeding Admin users...");

    var users = new List<User>();

    if (existingAdmins.All(u => u.Email.Value != "lucas@oriondev.com.br")) {
      users.Add(UserSeedHelper.CreateAdmin(
        "lucas@oriondev.com.br",
        "Lucas Admin",
        "richards.gt",
        "Admin@123",
        encryptionService
      ));
    }

    context.Users.AddRange(users);
    context.SaveChanges();

    Console.WriteLine($"--> Seeded {users.Count} admin user(s).");
  }
}
