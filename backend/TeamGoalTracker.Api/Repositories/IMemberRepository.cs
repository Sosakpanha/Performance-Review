using TeamGoalTracker.Api.Models;

namespace TeamGoalTracker.Api.Repositories;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(int id);
    Task UpdateMoodAsync(int id, string mood);
    Task<Dictionary<string, int>> GetMoodCountsAsync();
}
