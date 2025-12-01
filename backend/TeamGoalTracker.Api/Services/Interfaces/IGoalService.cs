using TeamGoalTracker.Api.DTOs;

namespace TeamGoalTracker.Api.Services.Interfaces;

public interface IGoalService
{
    Task<GoalDto> CreateGoalAsync(int memberId, string description);
    Task ToggleGoalAsync(int id);
    Task DeleteGoalAsync(int id);
}
