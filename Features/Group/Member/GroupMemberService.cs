
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Domain.Entities.Enums;
using FriendStuff.Features.Auth.DTOs;
using FriendStuff.Features.Group.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.Group.Member;

public class GroupMemberService(FriendStuffDbContext context, UserManager<User> userManager) : IGroupMemberService
{
    public async Task AddMember(string memberUsername, string groupName, string adminUsername)
    {

        var user = await userManager.FindByNameAsync(memberUsername)
                   ?? throw new ArgumentException("User not found");

        var admin = await userManager.FindByNameAsync(adminUsername)
                    ?? throw new ArgumentException("Admin not found");

        var group = await context.UserGroups.FirstOrDefaultAsync(g => g.NormalizeGroupName == groupName && g.AdminId == admin.Id)
                    ?? throw new ArgumentException("Group not found");

        if (user.UserGroups != null && user.UserGroups.Select(g => g.GroupId == group.GroupId).FirstOrDefault())
        {
            throw new ArgumentException("User already added");
        }

        GroupMember groupMember = new()
        {
            JoinDate = DateTime.UtcNow,
            Role = MemberRole.Member,
            GroupId = group.GroupId,
            UserId = user.Id
        };
        await context.GroupMembers.AddAsync(groupMember);
        await context.SaveChangesAsync();
    }

    public async Task RemoveMember(string memberUsername, string groupName, string adminUsername)
    {

        var user = await userManager.FindByNameAsync(memberUsername)
                   ?? throw new ArgumentException("User not found");

        var admin = await userManager.FindByNameAsync(adminUsername)
                    ?? throw new ArgumentException("Admin not found");

        var group = await context.UserGroups
            .Where(g => g.NormalizeGroupName.Equals(groupName) && g.AdminId == admin.Id)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Group not found");
        
        var groupMember = await context.GroupMembers.FirstOrDefaultAsync(gm => gm.GroupId == group.GroupId && gm.UserId == user.Id)
                          ?? throw new ArgumentException("User is not a member of this group");
        
        context.GroupMembers.Remove(groupMember);
        await context.SaveChangesAsync();
    }

    public async Task<UserInfoDto> SearchUser(string username)
    {
        var user = await userManager.FindByNameAsync(username) ?? throw new ArgumentException("User not found");
        var userInfo = new UserInfoDto
        { 
            Email = user.Email, 
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
        return userInfo;
    } 

}
