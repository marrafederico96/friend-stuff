using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Domain.Entities;

public class ExpenseContribution
{
    [Key]
    public Guid ExpenseContributionId { get; init; }

    [Required] 
    [MaxLength(255)]
    public string ParticipantId { get; set; } = string.Empty;

    [Required]
    public Guid ExpenseId { get; init; }

    [Required]
    public decimal AmountOwed { get; set; }

    [ForeignKey(name: "ParticipantId")]
    public User Participant { get; init; } = null!;

    [ForeignKey(name: "ExpenseId")]
    public Expense Expense { get; init; } = null!;
}
