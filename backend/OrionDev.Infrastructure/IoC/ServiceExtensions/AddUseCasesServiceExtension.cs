using Microsoft.Extensions.DependencyInjection;
using OrionDev.Application.UseCases.Auth;
using OrionDev.Application.UseCases.Comment;
using OrionDev.Application.UseCases.Expedition;
using OrionDev.Application.UseCases.Mission;
using OrionDev.Application.UseCases.Moon;
using OrionDev.Application.UseCases.MissionModule;
using OrionDev.Application.UseCases.Planet;
using OrionDev.Application.UseCases.SolarSystem;

namespace OrionDev.Infrastructure.IoC.ServiceExtensions;

public static class AddUseCasesServiceExtension {
  public static IServiceCollection AddUseCases(this IServiceCollection services) {
    // Auth
    services.AddScoped<Login>();
    services.AddScoped<RegisterUser>();

    // Solar System
    services.AddScoped<CreateSolarSystem>();
    services.AddScoped<DeleteSolarSystem>();
    services.AddScoped<GetAllSolarSystems>();
    services.AddScoped<GetByIdSolarSystem>();
    services.AddScoped<UpdateSolarSystem>();

    // Planet
    services.AddScoped<CreatePlanet>();
    services.AddScoped<DeletePlanet>();
    services.AddScoped<GetAllPlanets>();
    services.AddScoped<GetByIdPlanet>();
    services.AddScoped<UpdatePlanet>();

    // Mission
    services.AddScoped<CreateMission>();
    services.AddScoped<DeleteMission>();
    services.AddScoped<GetAllMissions>();
    services.AddScoped<GetByIdMission>();
    services.AddScoped<UpdateMission>();

    // Moon
    services.AddScoped<CreateMoon>();
    services.AddScoped<DeleteMoon>();
    services.AddScoped<GetAllMoons>();
    services.AddScoped<GetByIdMoon>();
    services.AddScoped<UpdateMoon>();

    // Mission Module
    services.AddScoped<CreateMissionModule>();
    services.AddScoped<DeleteMissionModule>();
    services.AddScoped<GetAllMissionModules>();
    services.AddScoped<GetByIdMissionModule>();
    services.AddScoped<UpdateMissionModule>();

    // Expedition
    services.AddScoped<CreateExpedition>();
    services.AddScoped<DeleteExpedition>();
    services.AddScoped<GetAllExpeditions>();
    services.AddScoped<GetByIdExpedition>();
    services.AddScoped<UpdateExpedition>();

    // Comment
    services.AddScoped<CreateCommentOnVideo>();
    services.AddScoped<DeleteCommentOnVideo>();
    services.AddScoped<EditCommentOnVideo>();

    return services;
  }
}
