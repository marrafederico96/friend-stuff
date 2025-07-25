using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Domain.Entities.Enums;
using FriendStuff.Features.Auth.DTOs;
using FriendStuff.Features.Group.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.Group;

public class GroupService(FriendStuffDbContext context, UserManager<User> userManager) : IGroupService
{
    public async Task CreateGroup(GroupDto groupDto)
    {
        var admin = await userManager.FindByNameAsync(groupDto.AdminUsername)
                    ?? throw new ArgumentException("Admin not found");

        UserGroup newGroup = new()
        {
            GroupName = groupDto.GroupName,
            NormalizeGroupName = groupDto.GroupName.TrimEnd().TrimStart().ToLowerInvariant(),
            CreatedAt = DateTime.UtcNow,
            AdminId = admin.Id,
        };

        await context.UserGroups.AddAsync(newGroup);
        await context.SaveChangesAsync();

        var group = await context.UserGroups
        .Where(g => g.NormalizeGroupName.Equals(newGroup.NormalizeGroupName) && g.AdminId == admin.Id)
        .FirstOrDefaultAsync() ?? throw new ArgumentException("Group not found");

        GroupMember groupMember = new()
        {
            JoinDate = DateTime.UtcNow,
            Role = MemberRole.Admin,
            GroupId = group.GroupId,
            UserId = admin.Id,
        };
        await context.GroupMembers.AddAsync(groupMember);
        await context.SaveChangesAsync();
    }

    public async Task DeleteGroup(GroupDto groupDto)
    {
        var admin = await userManager.FindByNameAsync(groupDto.AdminUsername.Trim().ToLowerInvariant())
                    ?? throw new ArgumentException("Admin not found");

        var normalizedGroupName = groupDto.GroupName.Trim().ToLowerInvariant();

        var group = await context.UserGroups
            .Where(g => g.NormalizeGroupName == normalizedGroupName && g.AdminId == admin.Id)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Group not found");

        context.UserGroups.Remove(group);
        await context.SaveChangesAsync();
    }

    public async Task<List<GroupInfoDto>> GetGroupInfo(string username)
    {
        var user = await context.Users
            .Include(u => u.UserGroups)
            .ThenInclude(g => g.Group).ThenInclude(userGroup => userGroup.GroupUsers)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("User not found");

        var groups = user.UserGroups.ToList();

        if (groups.Count == 0)
        {
            throw new ArgumentException("User has no groups");
        }

        var result = groups.Select(g => new GroupInfoDto
        {
            GroupName = g.Group.GroupName,
            Members = [.. g.Group.GroupUsers]
        }).ToList();

        return result;
    }
}
