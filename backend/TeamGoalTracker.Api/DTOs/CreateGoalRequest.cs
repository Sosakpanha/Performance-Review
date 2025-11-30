namespace TeamGoalTracker.Api.DTOs;

public class CreateGoalRequest
{
    public int MemberId { get; set; }
    public string Description { get; set; } = string.Empty;
}
