using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FriendStuff.Domain.Entities;

public class User : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public override required string UserName { get; set; }

    [Required] [MaxLength(100)] 
    public override required string NormalizedUserName { get; set; }

    [Required] [MaxLength(100)] 
    public override string? NormalizedEmail { get; set; }
    
    [Required]
    [MaxLength(100)]
    public override required string Email { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string FirstName { get; init; }

    [Required]
    [MaxLength(100)]
    public required string LastName { get; init; }

    public ICollection<GroupMember> UserGroups { get; init; } = [];

    public ICollection<Expense> Expenses { get; init; } = [];
    public ICollection<ExpenseContribution> ExpenseContributions { get; init; } = [];
    public ICollection<Refund> RefundsPaid { get; init; } = [];
    public ICollection<Refund> RefundsReceived { get; init; } = [];

}
