using OrionDev.Application.DTOs;
using OrionDev.Application.Mappers;
using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.Expedition;

public class GetByIdExpedition(IExpeditionRepository repository) {
  public async Task<GetByIdExpeditionOutput> Execute(Guid id) {
    var expedition = await repository.GetByIdAsync(id);
    var dto = ExpeditionMapper.ToDto(expedition);
    return new GetByIdExpeditionOutput(dto);
  }
}

public record GetByIdExpeditionOutput(ExpeditionDto Expedition);
