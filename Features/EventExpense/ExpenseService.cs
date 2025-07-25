using FriendStuff.Data;
using FriendStuff.Domain.Entities;
using FriendStuff.Features.EventExpense.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FriendStuff.Features.EventExpense;

public class ExpenseService(FriendStuffDbContext context, UserManager<User> userManager) : IExpenseService
{
    public async Task AddExpense(ExpenseDto expenseDto)
    {
        var normalizeEventName = expenseDto.EventName.TrimEnd().TrimStart().ToLowerInvariant();
        var payer = await userManager.FindByNameAsync(expenseDto.PayerUsername.Trim().ToLowerInvariant())
                    ?? throw new ArgumentException("User not found");

        var eventFound = await context.Events
        .Where(e => e.NormalizeEventName.Equals(normalizeEventName))
        .FirstOrDefaultAsync() ?? throw new ArgumentException("Event not found");


        Expense newExpense = new()
        {
            Amount = expenseDto.Amount,
            ExpenseName = expenseDto.ExpenseName,
            Description = expenseDto.Description,
            PayerId = payer.Id,
            EventId = eventFound.EventId,
        };

        await context.Expenses.AddAsync(newExpense);
        await context.SaveChangesAsync();

        var normalizedParticipants = expenseDto.ParticipantUsername.Select(u => u.Trim().ToLowerInvariant()).Distinct().ToList();
        var participants = normalizedParticipants.Count + 1;
        
        var expenseContributions = new List<ExpenseContribution>();
        foreach (var username in normalizedParticipants)
        {
            var user = await userManager.FindByNameAsync(username) ?? throw new ArgumentException($"User {username} not found");

            expenseContributions.Add(new ExpenseContribution
            {
                ExpenseId = newExpense.ExpenseId,
                AmountOwed = Math.Round(newExpense.Amount / participants, 2, MidpointRounding.AwayFromZero),
                ParticipantId = user.Id
            });
        }

        await context.ExpenseContributions.AddRangeAsync(expenseContributions);
        await context.SaveChangesAsync();
    }

    public async Task<decimal> CalculateBalance(BalanceDto balanceDto)
    {
        var payer = await userManager.Users
            .Where(u => u.NormalizedUserName != null && u.NormalizedUserName.Equals(balanceDto.PayerUsername.Trim(), StringComparison.InvariantCultureIgnoreCase))
            .Include(u => u.ExpenseContributions).ThenInclude(ec => ec.Expense)
            .Include(u => u.RefundsPaid)
            .Include(u => u.RefundsReceived)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Payer not found");

        var debtor = await userManager.Users
            .Where(u => u.NormalizedUserName != null && u.NormalizedUserName.Equals(balanceDto.DebtorUsername.Trim(), StringComparison.InvariantCultureIgnoreCase))
            .Include(u => u.ExpenseContributions).ThenInclude(ec => ec.Expense)
            .FirstOrDefaultAsync() ?? throw new ArgumentException("Debtor not found");

        var debtorOwesPayer = debtor.ExpenseContributions.Where(ec => ec.Expense.PayerId == payer.Id).Sum(ec => ec.AmountOwed);
        var payerOwesDebtor = payer.ExpenseContributions.Where(ec => ec.Expense.PayerId == debtor.Id).Sum(ec => ec.AmountOwed);

        var refundToPayer = payer.RefundsPaid.Where(r => r.DebtorId == debtor.Id).Sum(r => r.Amount);
        var refundToDebtor = payer.RefundsReceived.Where(r => r.DebtorId == payer.Id).Sum(r => r.Amount);

        return debtorOwesPayer - refundToPayer - payerOwesDebtor + refundToDebtor;
    }
}
