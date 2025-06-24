using Unistream.Domain.Abstractions.Services;
using Unistream.Domain.Exceptions;

namespace Unistream.Domain.Entities.Transaction;

public sealed class TransactionService : ITransactionService
{
    public void Validate(BaseTransaction transaction)
    {
        if (transaction.Amount < 1)
            throw new TransactionValidationException(transaction.Id, "Amount must be greater than 0");

        if (transaction.DateTime > DateTime.Now)
            throw new TransactionValidationException(transaction.Id, "Transaction date must be in the past");
    }
}
