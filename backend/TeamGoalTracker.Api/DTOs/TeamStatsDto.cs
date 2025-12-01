namespace TeamGoalTracker.Api.DTOs;

public class TeamStatsDto
{
    public int TotalGoals { get; set; }
    public int CompletedGoals { get; set; }
    public double CompletionPercentage { get; set; }
    public List<MoodCountDto> MoodCounts { get; set; } = new();
}
