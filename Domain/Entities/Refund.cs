using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendStuff.Domain.Entities;

public class Refund
{
    [Key]
    public Guid RefundId { get; init; }

    [Required] [MaxLength(255)]
    public string PayerId { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string DebtorId { get; set; } = string.Empty;

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime RefundDate { get; init; }

    [ForeignKey(name: "PayerId")]
    public User Payer { get; set; } = null!;

    [ForeignKey(name: "DebtorId")]
    public User Debtor { get; set; } = null!;

}
