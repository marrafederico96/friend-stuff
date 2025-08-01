using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Domain.Entities;

public class Location
{
    [Key]
    public Guid LocationId { get; init; }

    [Required]
    [MaxLength(100)]
    public required string LocationName { get; init; }

    [Required]
    [MaxLength(100)]
    public required string NormalizeLocationName { get; init; }

    public ICollection<Event> Events { get; set; } = [];
}
