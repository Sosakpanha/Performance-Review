namespace TeamGoalTracker.Api.Models;

public class Goal
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
