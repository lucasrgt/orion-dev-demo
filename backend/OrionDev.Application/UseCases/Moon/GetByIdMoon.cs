using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Moon;

public class GetByIdMoon(IMoonRepository repository) {
  public async Task<GetByIdMoonOutput> Execute(Guid id) {
    var moon = await repository.GetByIdAsync(id);
    var dto = MoonMapper.ToDto(moon);
    return new GetByIdMoonOutput(dto);
  }
}

public record GetByIdMoonOutput(MoonDto Moon);
