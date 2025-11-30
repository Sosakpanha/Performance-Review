using TeamGoalTracker.Api.Models;

namespace TeamGoalTracker.Api.Repositories;

public interface IGoalRepository
{
    Task<IEnumerable<Goal>> GetByMemberIdAsync(int memberId);
    Task<IEnumerable<Goal>> GetAllAsync();
    Task<Goal?> GetByIdAsync(int id);
    Task<Goal> CreateAsync(int memberId, string description);
    Task ToggleAsync(int id);
    Task DeleteAsync(int id);
    Task<(int total, int completed)> GetStatsAsync();
}
