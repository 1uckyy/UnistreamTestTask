using Unistream.Domain.Exceptions.Base;

namespace Unistream.Domain.Exceptions;

public sealed class NotEnoughFundsException : BadRequestException
{
    public NotEnoughFundsException(Guid clientId)
        : base("Not enough funds", $"Client {clientId} has not enough funds.")
    {
    }
}
