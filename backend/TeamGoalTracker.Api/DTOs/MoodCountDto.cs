namespace TeamGoalTracker.Api.DTOs;

public class MoodCountDto
{
    public string Mood { get; set; } = string.Empty;
    public string Emoji { get; set; } = string.Empty;
    public int Count { get; set; }
}
