using TeamGoalTracker.Api.DTOs;
using TeamGoalTracker.Api.Models;
using TeamGoalTracker.Api.Repositories.Interfaces;
using TeamGoalTracker.Api.Services.Interfaces;

namespace TeamGoalTracker.Api.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly IGoalRepository _goalRepository;

    public MemberService(IMemberRepository memberRepository, IGoalRepository goalRepository)
    {
        _memberRepository = memberRepository;
        _goalRepository = goalRepository;
    }

    public async Task<IEnumerable<MemberDto>> GetAllMembersWithGoalsAsync()
    {
        var members = await _memberRepository.GetAllAsync();
        var allGoals = await _goalRepository.GetAllAsync();
        var goalsByMember = GetGoalsByMemberDictionary(allGoals);

        return members.Select(m => GetMemberDtoResponse(m, goalsByMember.GetValueOrDefault(m.Id, new())));
    }

    public async Task<MemberDto?> GetMemberWithGoalsAsync(int id)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        if (member == null) return null;

        var goals = await _goalRepository.GetByMemberIdAsync(id);
        var goalsList = goals.ToList();

        return GetMemberDtoResponse(member, goalsList);
    }

    public async Task UpdateMoodAsync(int id, string mood)
    {
        await _memberRepository.UpdateMoodAsync(id, mood);
    }

    private static Dictionary<int, List<Goal>> GetGoalsByMemberDictionary(IEnumerable<Goal> goals)
    {
        return goals.GroupBy(g => g.MemberId)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    private static MemberDto GetMemberDtoResponse(Member member, List<Goal> memberGoals)
    {
        return new MemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Mood = member.Mood,
            MoodEmoji = MemberDto.GetMoodEmoji(member.Mood),
            Goals = GetGoalDtoList(memberGoals),
            CompletedCount = memberGoals.Count(g => g.IsCompleted),
            TotalCount = memberGoals.Count
        };
    }

    private static List<GoalDto> GetGoalDtoList(List<Goal> goals)
    {
        return goals.Select(g => new GoalDto
        {
            Id = g.Id,
            MemberId = g.MemberId,
            Description = g.Description,
            IsCompleted = g.IsCompleted
        }).ToList();
    }
}
