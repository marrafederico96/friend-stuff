using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FriendStuff.Domain.Entities.Enums;

namespace FriendStuff.Domain.Entities;

public class GroupMember
{
    [Key]
    public Guid GroupMemberId { get; init; }

    [Required]
    [MaxLength(255)]
    public string UserId { get; init; } = string.Empty;

    [Required]
    public Guid GroupId { get; init; }

    [Required]
    public required DateTime JoinDate { get; set; }

    public required MemberRole Role { get; set; }

    [ForeignKey(name: "UserId")]
    public User? UserMember { get; set; }

    [ForeignKey(name: "GroupId")]
    public UserGroup Group { get; set; } = null!;
}
