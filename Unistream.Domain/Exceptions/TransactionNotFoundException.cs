using Unistream.Domain.Exceptions.Base;

namespace Unistream.Domain.Exceptions;

public sealed class TransactionNotFoundException : NotFoundException
{
    public TransactionNotFoundException(Guid id)
        : base("Transaction not found", $"Transaction with identifier {id} was not found.")
    {
    }
}
