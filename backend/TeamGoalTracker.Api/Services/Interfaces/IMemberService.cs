using TeamGoalTracker.Api.DTOs;

namespace TeamGoalTracker.Api.Services.Interfaces;

public interface IMemberService
{
    Task<IEnumerable<MemberDto>> GetAllMembersWithGoalsAsync();
    Task<MemberDto?> GetMemberWithGoalsAsync(int id);
    Task UpdateMoodAsync(int id, string mood);
}
