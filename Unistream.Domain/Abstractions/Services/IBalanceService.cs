using Unistream.Domain.Entities.Balance;

namespace Unistream.Domain.Abstractions.Services;

public interface IBalanceService
{
    bool IsTransactionAlreadyReverted(Balance balance, Guid transactionId, out TransactionReverted? revertedEvent);

    decimal GetAmount(Balance balance);
}
