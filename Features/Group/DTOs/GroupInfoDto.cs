using System;
using System.ComponentModel.DataAnnotations;
using FriendStuff.Domain.Entities;

namespace FriendStuff.Features.Group.DTOs;

public record GroupInfoDto
{
    [Required]
    public required string GroupName { get; set; }

    [Required]
    public required List<GroupMember> Members { get; set; }

}
