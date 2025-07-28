using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.EventExpense.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.EventExpense.ExpenseRefund;

public class RefundService(UserManager<User> userManager,FriendStuffDbContext context, IExpenseService expenseService) : IRefundService
{
    public async Task AddRefund(RefundDto refundDto)
    {
        var payer = await userManager.FindByNameAsync(refundDto.PayerUsername)
                 ?? throw new ArgumentException("Payer not found");


        var debtor = await userManager.FindByNameAsync(refundDto.DebtorUsername)
                     ?? throw new ArgumentException("Payer not found");


        var totalDebt = await context.ExpenseContributions
            .Include(ec => ec.Expense)
            .Where(ec => ec.ParticipantId == debtor.Id && ec.Expense.PayerId == payer.Id && ec.AmountOwed > 0)
            .SumAsync(c => c.AmountOwed);

        var balanceDto = new BalanceDto
        {
            PayerUsername = payer.UserName,
            DebtorUsername = debtor.UserName
        };
        var balance = await expenseService.CalculateBalance(balanceDto);
        
        if (refundDto.Amount > balance)
        {
            throw new InvalidOperationException("Refund amount exceeds current debt");
        }

        Refund newRefund = new()
        {
            Amount = refundDto.Amount,
            DebtorId = debtor.Id,
            PayerId = payer.Id,
            RefundDate = DateTime.UtcNow
        };
        await context.Refunds.AddAsync(newRefund);
        await context.SaveChangesAsync();
    }

}
