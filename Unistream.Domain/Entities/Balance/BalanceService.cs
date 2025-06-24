using Unistream.Domain.Abstractions.Services;

namespace Unistream.Domain.Entities.Balance;

public sealed class BalanceService : IBalanceService
{
    public bool IsTransactionAlreadyReverted(Balance balance, Guid transactionId, out TransactionReverted? revertedEvent)
    {
        revertedEvent = balance.Events
            .FirstOrDefault(e => e is TransactionReverted reverted && reverted.RevertedTransactionId == transactionId) as TransactionReverted;

        return revertedEvent is not null;
    }

    public decimal GetAmount(Balance balance)
    {
        Dictionary<Guid, decimal> transactionsApplied = new();
        decimal currentBalance = 0;

        foreach (var @event in balance.Events.OrderBy(e => e.Timestamp))
        {
            switch (@event)
            {
                case FundsCredited credited:
                    currentBalance += credited.Amount;
                    transactionsApplied[credited.TransactionId] = credited.Amount;
                    break;
                case FundsDebited debited:
                    currentBalance -= debited.Amount;
                    transactionsApplied[debited.TransactionId] = -1 * debited.Amount;
                    break;
                case TransactionReverted reverted:
                    var appliedValue = transactionsApplied[reverted.RevertedTransactionId];
                    currentBalance += -1 * appliedValue;
                    break;
                default:
                    throw new InvalidDataException($"Unknown event type: {@event.GetType().Name}");
            }
        }

        return currentBalance;
    }
}
