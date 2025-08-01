using System.ComponentModel.DataAnnotations;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.Group.DTOs;
using FriendStuff.Features.GroupEvent.DTOs;

namespace FriendStuff.Features.Auth.DTOs;

public record UserInfoDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required] 
    public List<GroupDto> UserGroups { get; set; } = [];

}
