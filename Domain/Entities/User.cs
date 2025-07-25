using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FriendStuff.Domain.Entities;

public class User : IdentityUser
{
    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }

    public ICollection<GroupMember> UserGroups { get; set; } = [];

    public ICollection<Expense> Expenses { get; set; } = [];
    public ICollection<ExpenseContribution> ExpenseContributions { get; set; } = [];
    public ICollection<Refund> RefundsPaid { get; set; } = [];
    public ICollection<Refund> RefundsReceived { get; set; } = [];

}
