using Microsoft.AspNetCore.Mvc;
using OrionDev.Application.UseCases.Planet;

namespace OrionDev.AdminAPI.Controllers;

[Route("api/planets")]
[ApiController]
public class PlanetController(
  CreatePlanet createPlanetUc,
  DeletePlanet deletePlanetUc,
  GetAllPlanets getAllPlanetsUc,
  GetByIdPlanet getByIdPlanetUc,
  UpdatePlanet updatePlanetUc) : ControllerBase {
  [HttpGet]
  public async Task<IActionResult> GetAllPlanets() {
    var output = await getAllPlanetsUc.Execute();
    return Ok(output);
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetByIdPlanet(Guid id) {
    var output = await getByIdPlanetUc.Execute(id);
    return Ok(output);
  }

  [HttpPost]
  public async Task<IActionResult> CreatePlanet(CreatePlanetInput req) {
    var output = await createPlanetUc.Execute(req);
    return Ok(output);
  }

  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdatePlanet(Guid id, UpdatePlanetInput req) {
    var input = new UpdatePlanetInput(id.ToString(), req.Name, req.Lore, req.Appearance);
    var output = await updatePlanetUc.Execute(input);
    return Ok(output);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeletePlanet(Guid id) {
    var output = await deletePlanetUc.Execute(id);
    return Ok(output);
  }
}
