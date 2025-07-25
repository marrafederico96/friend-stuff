using System;
using FriendStuff.Features.Group.DTOs;

namespace FriendStuff.Features.Group.Member;

public interface IGroupMemberService
{

    /// <summary>
    /// Add user to group
    /// </summary>
    /// <param name="groupMemberDto">Object rappresent group member info</param>
    /// <returns></returns>
    public Task AddMember(GroupMemberDto groupMemberDto);

    /// <summary>
    /// Remove use to group
    /// </summary>
    /// <param name="groupMemberDto">Object rappresent group member info</param>
    /// <returns></returns>
    public Task RemoveMember(GroupMemberDto groupMemberDto);


}
