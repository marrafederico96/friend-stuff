using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.EventExpense.DTOs;

public record ExpenseDto
{

    [Required(ErrorMessage = "Amount cannot be empty")]
    public required decimal Amount { get; init; }

    [Required(ErrorMessage = "Expense name cannot be empty")]
    public required string ExpenseName { get; init; }

    [Required]
    public required string PayerUsername { get; init; }

    [Required]
    public required string EventName { get; set; }

    [Required]
    public required List<string> ParticipantUsername { get; set; }

    [MaxLength(100)]
    public string Description { get; init; } = string.Empty;

}
