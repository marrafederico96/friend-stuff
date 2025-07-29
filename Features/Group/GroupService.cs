using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Domain.Entities.Enums;
using FriendStuff.Features.Auth.DTOs;
using FriendStuff.Features.Group.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.Group;

public partial class GroupService(FriendStuffDbContext context, UserManager<User> userManager) : IGroupService
{
    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();

    [GeneratedRegex(@"[^a-z0-9\-]")]
    private static partial Regex InvalidCharsRegex();
    
    
    public async Task CreateGroup(GroupDto groupDto)
    {
        var admin = await userManager.FindByNameAsync(groupDto.AdminUsername)
                    ?? throw new ArgumentException("Admin not found");
        
        if (string.IsNullOrEmpty(groupDto.GroupName))
        {
            throw new ArgumentException("Group Name cannot be empty");
        }

        UserGroup newGroup = new()
        {
            GroupName = groupDto.GroupName,
            NormalizeGroupName = NormalizeGroupName(groupDto.GroupName),
            CreatedAt = DateTime.UtcNow,
            AdminId = admin.Id,
        };

        var existingGroup = await context.UserGroups
            .FirstOrDefaultAsync(g => g.NormalizeGroupName == newGroup.NormalizeGroupName
                                      && g.AdminId == newGroup.AdminId);
        if (existingGroup != null)
        {
            throw new ArgumentException("Group already exists");
        }
        
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
            .FirstOrDefaultAsync(u => u.UserName == username) ?? throw new ArgumentException("User not found");

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

    public async Task<GroupInfoDto> GetGroup(string groupName)
    {
        var group = await context.UserGroups
            .Where(g => g.NormalizeGroupName.Equals(NormalizeGroupName(groupName)))
            .Include(userGroup => userGroup.GroupUsers)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Group not found");

        var groupInfoDto = new GroupInfoDto
        {
            GroupName = group.GroupName,
            Members = group.GroupUsers.ToList()
        };
        return groupInfoDto;
    }
    
    private static string NormalizeGroupName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return string.Empty;

        var trimmed = name.TrimEnd().TrimStart().ToLowerInvariant();
        trimmed =  WhitespaceRegex().Replace(trimmed, "-");
        trimmed = InvalidCharsRegex().Replace(trimmed, "");

        return trimmed;
    }
}
