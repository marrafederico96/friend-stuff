using System.ComponentModel.DataAnnotations;
using FriendStuff.Domain.Entities.Enums;

namespace FriendStuff.Features.GroupEvent.DTOs;

public record class EventDto
{
    [Required(ErrorMessage = "Event name cannot be empty")]
    public string EventName { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string GroupName { get; set; } = string.Empty;

    public string EventDescription { get; set; } = string.Empty;

    [Required(ErrorMessage = "Start date cannot be empty")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End date cannot be empty")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Location name cannot be empty")]
    public string LocationName { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;
    public string StreetName { get; set; } = string.Empty;
    public string StreetNumber { get; set; } = string.Empty;

}
