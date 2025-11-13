using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrionDev.Application.UseCases.SolarSystem;

namespace OrionDev.AdminAPI.Controllers;

[Route("api/solarsystems")]
[ApiController]
public class SolarSystemController(
  CreateSolarSystem createSolarSystemUc,
  DeleteSolarSystem deleteSolarSystemUc,
  GetAllSolarSystems getAllSolarSystemUc,
  GetByIdSolarSystem getByIdSolarSystemUc,
  UpdateSolarSystem updateSolarSystemUc
) : ControllerBase {
  [HttpGet]
  public async Task<IActionResult> GetAllSolarSystem() {
    var output = await getAllSolarSystemUc.Execute();
    return Ok(output);
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetByIdSolarSystem(Guid id) {
    var output = await getByIdSolarSystemUc.Execute(id);
    return Ok(output);
  }

  [HttpPost]
  public async Task<IActionResult> CreateSolarSystem(CreateSolarSystemInput req) {
    var output = await createSolarSystemUc.Execute(req);
    return Ok(output);
  }

  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateSolarSystem(Guid id, UpdateSolarSystemInput req) {
    var input = new UpdateSolarSystemInput(id.ToString(), req.Name);
    var output = await updateSolarSystemUc.Execute(input);
    return Ok(output);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteSolarSystem(Guid id) {
    var output = await deleteSolarSystemUc.Execute(id);
    return Ok(output);
  }
}
