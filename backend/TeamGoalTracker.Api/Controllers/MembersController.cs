using Microsoft.AspNetCore.Mvc;
using TeamGoalTracker.Api.DTOs;
using TeamGoalTracker.Api.Enums;
using TeamGoalTracker.Api.Services.Interfaces;

namespace TeamGoalTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetMembers()
    {
        var members = await _memberService.GetAllMembersWithGoalsAsync();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetMember(int id)
    {
        var member = await _memberService.GetMemberWithGoalsAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return Ok(member);
    }

    [HttpPut("{id}/mood")]
    public async Task<IActionResult> UpdateMemberMood(int id, [FromBody] UpdateMoodRequest request)
    {
        if (!IsValidMood(request.Mood))
        {
            return BadRequest("Invalid mood value");
        }

        await _memberService.UpdateMoodAsync(id, request.Mood);
        return NoContent();
    }

    private static bool IsValidMood(string mood)
    {
        return Enum.TryParse<MoodEnum>(mood, ignoreCase: true, out _);
    }
}
