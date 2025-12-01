using TeamGoalTracker.Api.DTOs;
using TeamGoalTracker.Api.Models;
using TeamGoalTracker.Api.Repositories.Interfaces;
using TeamGoalTracker.Api.Services.Interfaces;

namespace TeamGoalTracker.Api.Services;

public class GoalService : IGoalService
{
    private readonly IGoalRepository _goalRepository;

    public GoalService(IGoalRepository goalRepository)
    {
        _goalRepository = goalRepository;
    }

    public async Task<GoalDto> CreateGoalAsync(int memberId, string description)
    {
        var goal = await _goalRepository.CreateAsync(memberId, description);
        return GetGoalDtoResponse(goal);
    }

    public async Task ToggleGoalAsync(int id)
    {
        await _goalRepository.ToggleAsync(id);
    }

    public async Task DeleteGoalAsync(int id)
    {
        await _goalRepository.DeleteAsync(id);
    }

    private static GoalDto GetGoalDtoResponse(Goal goal)
    {
        return new GoalDto
        {
            Id = goal.Id,
            Description = goal.Description,
            IsCompleted = goal.IsCompleted
        };
    }
}
