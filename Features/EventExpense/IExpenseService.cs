using FriendStuff.Features.EventExpense.DTOs;

namespace FriendStuff.Features.EventExpense;

public interface IExpenseService
{
    /// <summary>
    /// Add expense to Event
    /// </summary>
    /// <param name="expenseDto">Object represents Expense info</param>
    /// <returns></returns>
    public Task AddExpense(ExpenseDto expenseDto);
    
    /// <summary>
    /// Calculate balance between two users.
    /// </summary>
    /// <param name="balanceDto">Object represents information of two users.</param>
    /// <returns></returns>
    public Task<decimal> CalculateBalance(BalanceDto balanceDto);

}
