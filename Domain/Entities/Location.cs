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


    [MaxLength(100)]
    public string City { get; init; } = string.Empty;


    [MaxLength(100)]
    public string StreetName { get; init; } = string.Empty;

    [MaxLength(50)]
    public string StreetNumber { get; init; } = string.Empty;

    public ICollection<Event> Events { get; set; } = [];
}
