namespace TeamGoalTracker.Api.Models;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Mood { get; set; } = "neutral";
}
