using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Planet;

public class GetAllPlanets(IPlanetRepository repository) {
  public async Task<GetAllPlanetsOutput> Execute() {
    var planets = await repository.GetAllAsync();
    var planetDtos = planets.Select(PlanetMapper.ToDto).ToList();
    return new GetAllPlanetsOutput(planetDtos);
  }
}

public record GetAllPlanetsOutput(List<PlanetDto> Planets);
