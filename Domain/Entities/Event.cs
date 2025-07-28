using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FriendStuff.Domain.Entities.Enums;

namespace FriendStuff.Domain.Entities;

public class Event
{
    [Key]
    public Guid EventId { get; set; }

    [Required]
    public Guid GroupId { get; set; }

    [Required]
    public Guid LocationId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string EventName { get; init; }

    [Required]
    [MaxLength(100)]
    public required string NormalizeEventName { get; init; }

    [Required]
    [MaxLength(255)]
    public required string EventDescription { get; init; }

    [Required]
    public required DateTime StartDate { get; init; }

    [Required]
    public required DateTime EndDate { get; init; }

    [ForeignKey(name: "GroupId")]
    public UserGroup? Group { get; set; }

    [ForeignKey(name: "LocationId")]
    public Location? Location { get; set; }

    public ICollection<Expense> Expenses { get; set; } = [];

}
