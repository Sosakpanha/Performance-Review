using TeamGoalTracker.Api.DTOs;

namespace TeamGoalTracker.Api.Services;

public interface IStatsService
{
    Task<TeamStatsDto> GetTeamStatsAsync();
}
