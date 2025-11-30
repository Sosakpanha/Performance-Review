using TeamGoalTracker.Api.DTOs;
using TeamGoalTracker.Api.Repositories;

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
        var goalsByMember = allGoals.GroupBy(g => g.MemberId)
            .ToDictionary(g => g.Key, g => g.ToList());

        return members.Select(m =>
        {
            var memberGoals = goalsByMember.GetValueOrDefault(m.Id, new());
            return new MemberDto
            {
                Id = m.Id,
                Name = m.Name,
                Mood = m.Mood,
                MoodEmoji = MemberDto.GetMoodEmoji(m.Mood),
                Goals = memberGoals.Select(g => new GoalDto
                {
                    Id = g.Id,
                    MemberId = g.MemberId,
                    Description = g.Description,
                    IsCompleted = g.IsCompleted
                }).ToList(),
                CompletedCount = memberGoals.Count(g => g.IsCompleted),
                TotalCount = memberGoals.Count
            };
        });
    }

    public async Task<MemberDto?> GetMemberWithGoalsAsync(int id)
    {
        var member = await _memberRepository.GetByIdAsync(id);
        if (member == null) return null;

        var goals = await _goalRepository.GetByMemberIdAsync(id);
        var goalsList = goals.ToList();

        return new MemberDto
        {
            Id = member.Id,
            Name = member.Name,
            Mood = member.Mood,
            MoodEmoji = MemberDto.GetMoodEmoji(member.Mood),
            Goals = goalsList.Select(g => new GoalDto
            {
                Id = g.Id,
                MemberId = g.MemberId,
                Description = g.Description,
                IsCompleted = g.IsCompleted
            }).ToList(),
            CompletedCount = goalsList.Count(g => g.IsCompleted),
            TotalCount = goalsList.Count
        };
    }

    public async Task UpdateMoodAsync(int id, string mood)
    {
        await _memberRepository.UpdateMoodAsync(id, mood);
    }
}
