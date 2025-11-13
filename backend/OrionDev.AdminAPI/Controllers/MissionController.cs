using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrionDev.Application.UseCases.Mission;

namespace OrionDev.AdminAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class MissionController(
  CreateMission createMissionUc,
  DeleteMission deleteMissionUc,
  GetAllMissions getAllMissionUc,
  GetByIdMission getByIdMissionUc,
  UpdateMission updateMissionUc
) : ControllerBase {
  [HttpPost("create-mission")]
  public async Task<IActionResult> CreateMission(CreateMissionInput req) {
    var output = await createMissionUc.Execute(req);
    return Ok(output);
  }

  [HttpDelete("delete-mission/{id:guid}")]
  public async Task<IActionResult> DeleteMission(Guid id) {
    var output = await deleteMissionUc.Execute(id);
    return Ok(output);
  }

  [HttpGet("get-all-missions")]
  public async Task<IActionResult> GetAllMissions() {
    var output = await getAllMissionUc.Execute();
    return Ok(output);
  }

  [HttpGet("get-by-id-mission/{id:guid}")]
  public async Task<IActionResult> GetByIdMission(Guid id) {
    var output = await getByIdMissionUc.Execute(id);
    return Ok(output);
  }
}
