using System.ComponentModel.DataAnnotations;
using FriendStuff.Domain.Entities.Enums;

namespace FriendStuff.Features.Group.DTOs;

public record GroupMemberDto
{
    [Required]
    public required string GroupName { get; set; }

    [Required]
    public required string Username { get; set; }

    [Required]
    public required string AdminUsername { get; set; }
    
    public MemberRole MemberRole { get; set; }
    
    public DateTime JoinDate { get; set; }

}
