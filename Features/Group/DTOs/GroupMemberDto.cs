using System.ComponentModel.DataAnnotations;
using FriendStuff.Domain.Entities.Enums;

namespace FriendStuff.Features.Group.DTOs;

public record GroupMemberDto
{
    [Required]
    public required string GroupName { get; init; }

    [Required]
    public required string Username { get; init; }

    [Required]
    public required string AdminUsername { get; init; }
    
    public MemberRole MemberRole { get; init; }
    
    public DateTime JoinDate { get; init; }

}
