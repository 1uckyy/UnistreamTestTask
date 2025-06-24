using Unistream.Domain.Exceptions.Base;

namespace Unistream.Domain.Exceptions;

public sealed class BalanceNotFoundException : NotFoundException
{
    public BalanceNotFoundException(Guid clientId)
        : base("Balance not found", $"Balance for client with identifier {clientId} was not found.")
    {
    }
}
