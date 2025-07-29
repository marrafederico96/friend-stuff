using System.ComponentModel.DataAnnotations;
using FriendStuff.Features.GroupEvent.DTOs;

namespace FriendStuff.Features.Group.DTOs;

public record  GroupDto
{
    [Required(ErrorMessage = "Group name cannot be empty")]
    public string GroupName { get; set; } = string.Empty;

    [Required] 
    public string NormalizeGroupName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Admin username cannot be empty")]
    public string AdminUsername { get; set; } = string.Empty;

    public List<EventDto?> GroupEvents { get; set; } = [];
    public List<GroupMemberDto> GroupMembers { get; set; } = [];

}
