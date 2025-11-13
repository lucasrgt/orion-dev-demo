using OrionDev.Domain.Repositories;

namespace OrionDev.Application.UseCases.SolarSystem;

public class UpdateSolarSystem(ISolarSystemRepository repository)
{
    public async Task<UpdateSolarSystemOutput> Execute(UpdateSolarSystemInput input)
    {
        var solarSystem = await repository.GetByIdAsync(Guid.Parse(input.Id));

        if (solarSystem == null)
        {
            throw new Exception("Sistema Solar não encontrado");
        }

        solarSystem.UpdateName(input.Name);
        solarSystem.UpdateUpdatedOn(DateTime.UtcNow);

        // EF já rastreia mudanças, só precisa do SaveAsync
        await repository.SaveAsync();

        return new UpdateSolarSystemOutput(solarSystem.Id.ToString(), solarSystem.Name);
    }
}

public record UpdateSolarSystemOutput(string Id, string Name);

public record UpdateSolarSystemInput(string Id, string Name);
