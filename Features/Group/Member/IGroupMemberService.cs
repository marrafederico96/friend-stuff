using System;
using FriendStuff.Features.Auth.DTOs;
using FriendStuff.Features.Group.DTOs;

namespace FriendStuff.Features.Group.Member;

public interface IGroupMemberService
{

    /// <summary>
    /// 
    /// </summary>
    /// <param name="memberUsername"></param>
    /// <param name="groupName"></param>
    /// <param name="adminUsername"></param>
    /// <returns></returns>
    public Task AddMember(string memberUsername, string groupName, string adminUsername);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="memberUsername"></param>
    /// <param name="groupName"></param>
    /// <param name="adminUsername"></param>
    /// <returns></returns>
    public Task RemoveMember(string memberUsername, string groupName, string adminUsername);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public Task<UserInfoDto> SearchUser(string username);



}
