using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Group.DTOs;

public record  GroupDto
{
    [Required(ErrorMessage = "Group name cannot be empty")]
    public string? GroupName { get; set; }

    [Required(ErrorMessage = "Admin username cannot be empty")]

    public string AdminUsername { get; set; } = string.Empty;


}
