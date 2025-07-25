using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.Group.DTOs;

public record GroupMemberDto
{
    [Required]
    public required string GroupName { get; init; }

    [Required]
    public required string Username { get; init; }

    [Required]
    public string? AdminUsername { get; init; }

}
