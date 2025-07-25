using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Domain.Entities;

public class Expense
{
    [Key]
    public Guid ExpenseId { get; init; }

    [Required]
    public required decimal Amount { get; init; }

    [Required]
    [MaxLength(100)]
    public required string ExpenseName { get; set; }

    [MaxLength(100)]
    public string Description { get; init; } = string.Empty;

    [Required] 
    [MaxLength(255)]
    public required string PayerId { get; set; } = string.Empty;
    [Required]
    public required Guid EventId { get; init; }

    [ForeignKey(name: "PayerId")]
    public User Payer { get; init; } = null!;

    [ForeignKey(name: "EventId")]
    public Event Event { get; init; } = null!;

    public ICollection<ExpenseContribution> Participants { get; set; } = [];
}
