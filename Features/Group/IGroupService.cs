using FriendStuff.Features.Group.DTOs;

namespace FriendStuff.Features.Group;

public interface IGroupService
{
    /// <summary>
    /// Create group of user
    /// </summary>
    /// <param name="groupDto">Object represent group information</param>
    /// <returns></returns>
    public Task CreateGroup(GroupDto groupDto);

    /// <summary>
    /// Delete group of user
    /// </summary>
    /// <param name="groupDto">Object represent group information</param>
    /// <returns></returns>
    public Task DeleteGroup(GroupDto groupDto);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="usernameDto"></param>
    /// <returns></returns>
    public Task<List<GroupInfoDto>> GetGroupInfo(string username);


}
