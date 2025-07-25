using System.ComponentModel.DataAnnotations;

namespace FriendStuff.Features.EventExpense.DTOs;

public record RefundDto
{

    [Required]
    public required string PayerUsername { get; init; }

    [Required]
    public required string DebtorUsername { get; init; }

    [Required]
    public required decimal Amount { get; init; }

}
