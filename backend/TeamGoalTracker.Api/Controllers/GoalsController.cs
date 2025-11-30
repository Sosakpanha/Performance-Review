using Microsoft.AspNetCore.Mvc;
using TeamGoalTracker.Api.DTOs;
using TeamGoalTracker.Api.Services;

namespace TeamGoalTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GoalsController : ControllerBase
{
    private readonly IGoalService _goalService;

    public GoalsController(IGoalService goalService)
    {
        _goalService = goalService;
    }

    [HttpPost]
    public async Task<ActionResult<GoalDto>> CreateGoal([FromBody] CreateGoalRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Description))
        {
            return BadRequest("Goal description is required");
        }

        var goal = await _goalService.CreateGoalAsync(request.MemberId, request.Description);
        return Created($"/api/goals/{goal.Id}", goal);
    }

    [HttpPut("{id}/toggle")]
    public async Task<IActionResult> ToggleGoal(int id)
    {
        await _goalService.ToggleGoalAsync(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(int id)
    {
        await _goalService.DeleteGoalAsync(id);
        return NoContent();
    }
}
