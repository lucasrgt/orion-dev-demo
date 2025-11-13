using Microsoft.AspNetCore.Mvc;
using OrionDev.Application.UseCases.Moon;

namespace OrionDev.AdminAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoonController(
  CreateMoon createMoonUc,
  DeleteMoon deleteMoonUc,
  GetAllMoons getAllMoonsUc,
  GetByIdMoon getByIdMoonUc,
  UpdateMoon updateMoonUc) : ControllerBase {
  [HttpPost("create-moon")]
  public async Task<IActionResult> CreateMoon(CreateMoonInput req) {
    var output = await createMoonUc.Execute(req);
    return Ok(output);
  }

  [HttpDelete("delete-moon/{id:guid}")]
  public async Task<IActionResult> DeleteMoon(Guid id) {
    var output = await deleteMoonUc.Execute(id);
    return Ok(output);
  }

  [HttpGet("get-all-moons")]
  public async Task<IActionResult> GetAllMoons() {
    var output = await getAllMoonsUc.Execute();
    return Ok(output);
  }

  [HttpGet("get-by-id-moon/{id:guid}")]
  public async Task<IActionResult> GetByIdMoon(Guid id) {
    var output = await getByIdMoonUc.Execute(id);
    return Ok(output);
  }
}
