using System.Text.RegularExpressions;
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.GroupEvent.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.GroupEvent;

public partial class EventService(FriendStuffDbContext context, UserManager<User> userManager) : IEventService
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    [GeneratedRegex(@"[^a-z0-9\-]")]
    private static partial Regex InvalidCharsRegex();
    public async Task CreateEvent(EventDto eventDto)
    {
        var groupName = eventDto.GroupName.TrimEnd().TrimStart().ToLowerInvariant();

        var user = await userManager.Users
                       .Include(u => u.UserGroups)
                       .ThenInclude(ug => ug.Group)
                       .FirstOrDefaultAsync(u => u.NormalizedUserName == eventDto.Username)
                   ?? throw new ArgumentException("User not found");

        var group = user.UserGroups
            .FirstOrDefault(g => g.Group.NormalizeGroupName.Equals(NormalizeEventName(groupName))) ?? throw new ArgumentException("Group not found");
        var result = await context.Events.Where(e => e.NormalizeEventName.Equals(NormalizeEventName(eventDto.EventName)) && e.GroupId == group.GroupId).FirstOrDefaultAsync();

        if (result != null)
        {
            throw new ArgumentException("Event name already exists");
        }

        if (eventDto.EndDate < eventDto.StartDate)
        {
            throw new ArgumentException("End Date invalid");
        }

        var location = await context.Locations
            .Where(l => l.NormalizeLocationName.Equals(eventDto.LocationName.TrimEnd().TrimStart().ToLowerInvariant()))
            .FirstOrDefaultAsync();


        if (location != null)
        {
            Event newEvent = new()
            {
                EndDate = DateTime.SpecifyKind(eventDto.EndDate, DateTimeKind.Utc),
                StartDate =DateTime.SpecifyKind(eventDto.StartDate, DateTimeKind.Utc),
                NormalizeEventName = NormalizeEventName(eventDto.EventName),
                EventDescription = eventDto.EventDescription,
                EventName = eventDto.EventName,
                GroupId = group.GroupId,
                LocationId = location.LocationId
            };
            await context.Events.AddAsync(newEvent);
        }
        else
        {
            Location newLocation = new()
            {
                City = eventDto.City,
                NormalizeLocationName = eventDto.LocationName.TrimEnd().TrimStart().ToLowerInvariant(),
                LocationName = eventDto.LocationName,
                StreetName = eventDto.StreetName,
                StreetNumber = eventDto.StreetNumber
            };
            await context.Locations.AddAsync(newLocation);
            await context.SaveChangesAsync();

            Event newEvent = new()
            {
                EndDate = eventDto.EndDate,
                NormalizeEventName = NormalizeEventName(eventDto.EventName),
                StartDate = eventDto.StartDate,
                EventDescription = eventDto.EventDescription,
                EventName = eventDto.EventName,
                GroupId = group.GroupId,
                LocationId = newLocation.LocationId
            };
            await context.Events.AddAsync(newEvent);
        }
        await context.SaveChangesAsync();
    }

    public async Task<List<EventDto>> GetEvents(string groupName)
    {
        var events = await context.Events
            .Include(e => e.Group)
            .Include(e => e.Location)
            .Where(e => e.Group != null && e.Group.NormalizeGroupName == groupName)
            .Select(e => new EventDto
            {
                GroupName = e.Group != null ? e.Group.GroupName : string.Empty,
                EndDate = e.EndDate,
                EventName = e.EventName,
                NormalizedEventName = e.NormalizeEventName,
                StartDate = e.StartDate,
                LocationName = e.Location != null ? e.Location.LocationName : string.Empty,
                City = e.Location != null ? e.Location.City : string.Empty,
                StreetName = e.Location != null ? e.Location.StreetName: string.Empty,
                StreetNumber = e.Location != null ? e.Location.StreetNumber: string.Empty,
                EventDescription = e.EventDescription,
            })
            .ToListAsync();

        return events;
    }
    
    private static string NormalizeEventName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        var trimmed = name.TrimEnd().TrimStart().ToLowerInvariant();
        trimmed =  WhitespaceRegex().Replace(trimmed, "-");
        trimmed = InvalidCharsRegex().Replace(trimmed, "");

        return trimmed;
    }

}
