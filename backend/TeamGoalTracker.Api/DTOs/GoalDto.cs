namespace TeamGoalTracker.Api.DTOs;

public class GoalDto
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
