using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.EventExpense.DTOs;

public record RefundDto
{

    [Required(ErrorMessage = "Payer Username required")] 
    public string PayerUsername { get; set; } = string.Empty;

    public string DebtorUsername { get; set; } = string.Empty;

    [Required(ErrorMessage = "Amount required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

}
