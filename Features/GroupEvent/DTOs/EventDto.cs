using System;
using System.ComponentModel.DataAnnotations;
using FriendStuff.Domain.Entities.Enums;

namespace FriendStuff.Features.GroupEvent.DTOs;

public record class EventDto
{
    [Required(ErrorMessage = "Event name cannot be empty")]
    public required string EventName { get; init; }

    [Required]
    public required string Username { get; init; }

    [Required]
    public required string GroupName { get; init; }

    public string EventDescription { get; init; } = string.Empty;

    [Required(ErrorMessage = "Start date cannot be empty")]
    public required DateOnly StartDate { get; init; }

    [Required(ErrorMessage = "End date cannot be empty")]
    public required DateOnly EndDate { get; init; }

    [Required(ErrorMessage = "Event category cannot be empty")]
    public required EventCategory EventCategory { get; init; }

    [Required(ErrorMessage = "Location name cannot be empty")]
    public required string LocationName { get; init; }

    public string City { get; init; } = string.Empty;
    public string StreetName { get; init; } = string.Empty;
    public string StreetNumber { get; init; } = string.Empty;

}
