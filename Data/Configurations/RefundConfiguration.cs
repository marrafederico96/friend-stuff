using FriendStuff.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FriendStuff.Data.Configurations;

public class RefundConfiguration : IEntityTypeConfiguration<Refund>
{
    public void Configure(EntityTypeBuilder<Refund> builder)
    {
        builder.HasOne(r => r.Payer)
            .WithMany(u => u.RefundsPaid)
            .HasForeignKey(r => r.PayerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Debtor)
            .WithMany(u => u.RefundsReceived)
            .HasForeignKey(r => r.DebtorId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
