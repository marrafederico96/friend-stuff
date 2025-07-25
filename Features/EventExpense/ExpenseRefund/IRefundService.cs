using FriendStuff.Features.EventExpense.DTOs;

namespace FriendStuff.Features.EventExpense.ExpenseRefund;

public interface IRefundService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="refundDto"></param>
    /// <returns></returns>
    public Task AddRefund(RefundDto refundDto);

}
