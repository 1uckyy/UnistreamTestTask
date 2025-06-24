using Unistream.Domain.Exceptions.Base;

namespace Unistream.Domain.Exceptions;

public sealed class TransactionValidationException : BadRequestException
{
    public TransactionValidationException(Guid id, string message)
        : base("Transaction is not valid", $"Transaction with identifier {id} is not valid. {message}")
    {
    }
}
