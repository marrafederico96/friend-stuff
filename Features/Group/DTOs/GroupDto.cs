using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Group.DTOs;

public record  GroupDto
{
    [Required(ErrorMessage = "Group name cannot be empty")]
    public string GroupName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Admin username cannot be empty")]

    public string AdminUsername { get; set; } = string.Empty;


}
