using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.EventExpense.DTOs;

public record ExpenseDto
{

    [Required(ErrorMessage = "Amount cannot be empty")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Expense name cannot be empty")]
    public string ExpenseName { get; set; } = string.Empty;

    [Required] 
    public string PayerUsername { get; set; } = string.Empty;

    [Required]
    public string EventName { get; set; } = string.Empty;

    [Required] 
    public List<string> ParticipantUsername { get; set; } = [];

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

}
