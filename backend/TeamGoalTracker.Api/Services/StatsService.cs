using TeamGoalTracker.Api.DTOs;
using TeamGoalTracker.Api.Repositories;

namespace TeamGoalTracker.Api.Services;

public class StatsService : IStatsService
{
    private readonly IGoalRepository _goalRepository;
    private readonly IMemberRepository _memberRepository;

    private static readonly Dictionary<string, string> MoodEmojis = new()
    {
        { "happy", "\ud83d\ude0a" },
        { "neutral", "\ud83d\ude10" },
        { "sad", "\ud83d\ude22" },
        { "stressed", "\ud83d\ude30" },
        { "excited", "\ud83e\udd29" }
    };

    public StatsService(IGoalRepository goalRepository, IMemberRepository memberRepository)
    {
        _goalRepository = goalRepository;
        _memberRepository = memberRepository;
    }

    public async Task<TeamStatsDto> GetTeamStatsAsync()
    {
        var (total, completed) = await _goalRepository.GetStatsAsync();
        var moodCounts = await _memberRepository.GetMoodCountsAsync();

        return new TeamStatsDto
        {
            TotalGoals = total,
            CompletedGoals = completed,
            CompletionPercentage = total > 0 ? Math.Round((double)completed / total * 100, 1) : 0,
            MoodCounts = moodCounts.Select(mc => new MoodCountDto
            {
                Mood = mc.Key,
                Emoji = MoodEmojis.GetValueOrDefault(mc.Key, "\u2753"),
                Count = mc.Value
            }).ToList()
        };
    }
}
