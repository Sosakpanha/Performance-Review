using Microsoft.AspNetCore.Mvc;
using TeamGoalTracker.Api.DTOs;
using TeamGoalTracker.Api.Services.Interfaces;

namespace TeamGoalTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _statsService;

    public StatsController(IStatsService statsService)
    {
        _statsService = statsService;
    }

    [HttpGet]
    public async Task<ActionResult<TeamStatsDto>> GetTeamStats()
    {
        var stats = await _statsService.GetTeamStatsAsync();
        return Ok(stats);
    }
}
