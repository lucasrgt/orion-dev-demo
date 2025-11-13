using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Moon;

public class GetAllMoons(IMoonRepository repository) {
  public async Task<GetAllMoonsOutput> Execute() {
    var moons = await repository.GetAllAsync();
    var moonsDtos = moons.Select(MoonMapper.ToDto).ToList();
    return new GetAllMoonsOutput(moonsDtos);
  }
}

public record GetAllMoonsOutput(List<MoonDto> Moons);
