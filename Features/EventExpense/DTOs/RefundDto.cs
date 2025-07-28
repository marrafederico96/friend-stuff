using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.EventExpense.DTOs;

public record RefundDto
{

    [Required] 
    public string PayerUsername { get; set; } = string.Empty;

    [Required]
    public string DebtorUsername { get; set; } = string.Empty;

    [Required] public decimal Amount { get; set; }

}
