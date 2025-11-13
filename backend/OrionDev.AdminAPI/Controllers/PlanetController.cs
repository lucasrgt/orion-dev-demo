using Microsoft.AspNetCore.Mvc;
using OrionDev.Application.UseCases.Planet;

namespace OrionDev.AdminAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlanetController(
  CreatePlanet createPlanetUc,
  DeletePlanet deletePlanetUc,
  GetAllPlanets getAllPlanetsUc,
  GetByIdPlanet getByIdPlanetUc,
  UpdatePlanet updatePlanetUc) : ControllerBase {
  [HttpPost("create-planet")]
  public async Task<IActionResult> CreatePlanet(CreatePlanetInput req) {
    var output = await createPlanetUc.Execute(req);
    return Ok(output);
  }

  [HttpDelete("delete-planet/{id:guid}")]
  public async Task<IActionResult> DeletePlanet(Guid id) {
    var output = await deletePlanetUc.Execute(id);
    return Ok(output);
  }

  [HttpGet("get-all-planets")]
  public async Task<IActionResult> GetAllPlanets() {
    var output = await getAllPlanetsUc.Execute();
    return Ok(output);
  }

  [HttpGet("get-by-id-planet/{id:guid}")]
  public async Task<IActionResult> GetByIdPlanet(Guid id) {
    var output = await getByIdPlanetUc.Execute(id);
    return Ok(output);
  }
}
