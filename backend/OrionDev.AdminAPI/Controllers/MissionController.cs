using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrionDev.Application.UseCases.Mission;

namespace OrionDev.AdminAPI.Controllers;

[Route("api/missions")]
[ApiController]
public class MissionController(
  CreateMission createMissionUc,
  DeleteMission deleteMissionUc,
  GetAllMissions getAllMissionUc,
  GetByIdMission getByIdMissionUc,
  UpdateMission updateMissionUc
) : ControllerBase {
  [HttpGet]
  public async Task<IActionResult> GetAllMissions() {
    var output = await getAllMissionUc.Execute();
    return Ok(output);
  }

  [HttpGet("{id:guid}")]
  public async Task<IActionResult> GetByIdMission(Guid id) {
    var output = await getByIdMissionUc.Execute(id);
    return Ok(output);
  }

  [HttpPost]
  public async Task<IActionResult> CreateMission(CreateMissionInput req) {
    var output = await createMissionUc.Execute(req);
    return Ok(output);
  }

  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateMission(Guid id, CreateMissionInput req) {
    var input = new UpdateMissionInput(
      id.ToString(),
      req.Name,
      req.ExpeditionsNumber,
      req.ExplorationTimeInMinutes,
      req.AreaCategory,
      req.Difficulty,
      req.Lore
    );
    var output = await updateMissionUc.Execute(input);
    return Ok(output);
  }

  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteMission(Guid id) {
    var output = await deleteMissionUc.Execute(id);
    return Ok(output);
  }
}
