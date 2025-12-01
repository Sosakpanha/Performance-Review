using TeamGoalTracker.Api.DTOs;
using TeamGoalTracker.Api.Repositories.Interfaces;
using TeamGoalTracker.Api.Services.Interfaces;

namespace TeamGoalTracker.Api.Services;

public class StatsService : IStatsService
{
    private static readonly Dictionary<string, string> MoodEmojis = new()
    {
        { "happy", "\ud83d\ude0a" },
        { "neutral", "\ud83d\ude10" },
        { "sad", "\ud83d\ude22" },
        { "stressed", "\ud83d\ude30" },
        { "excited", "\ud83e\udd29" }
    };

    private readonly IGoalRepository _goalRepository;
    private readonly IMemberRepository _memberRepository;

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
            CompletionPercentage = GetCompletionPercentage(total, completed),
            MoodCounts = GetMoodCountDtos(moodCounts)
        };
    }

    private static double GetCompletionPercentage(int total, int completed)
    {
        return total > 0 ? Math.Round((double)completed / total * 100, 1) : 0;
    }

    private static List<MoodCountDto> GetMoodCountDtos(Dictionary<string, int> moodCounts)
    {
        return moodCounts.Select(mc => new MoodCountDto
        {
            Mood = mc.Key,
            Emoji = GetMoodEmoji(mc.Key),
            Count = mc.Value
        }).ToList();
    }

    private static string GetMoodEmoji(string mood)
    {
        return MoodEmojis.GetValueOrDefault(mood, "\u2753");
    }
}
