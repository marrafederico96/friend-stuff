using FriendStuff.Features.GroupEvent.DTOs;

namespace FriendStuff.Features.GroupEvent;

public interface IEventService
{

    /// <summary>
    /// Create event into a group
    /// </summary>
    /// <param name="eventDto">Object represent Event information</param>
    /// <returns></returns>
    public Task CreateEvent(EventDto eventDto);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    public Task<List<EventDto>> GetEvents(string groupName);


}
