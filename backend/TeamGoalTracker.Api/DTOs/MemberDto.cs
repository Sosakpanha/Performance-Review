namespace TeamGoalTracker.Api.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Mood { get; set; } = string.Empty;
    public string MoodEmoji { get; set; } = string.Empty;
    public List<GoalDto> Goals { get; set; } = new();
    public int CompletedCount { get; set; }
    public int TotalCount { get; set; }

    public static string GetMoodEmoji(string mood) => mood switch
    {
        "happy" => "😊",
        "neutral" => "😐",
        "sad" => "😢",
        "stressed" => "😰",
        "excited" => "🎉",
        _ => "😐"
    };
}
