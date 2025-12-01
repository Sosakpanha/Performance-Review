using TeamGoalTracker.Api.DTOs;

namespace TeamGoalTracker.Api.Services.Interfaces;

public interface IStatsService
{
    Task<TeamStatsDto> GetTeamStatsAsync();
}
