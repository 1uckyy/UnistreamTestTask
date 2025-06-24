using Unistream.Domain.Abstractions;

namespace Unistream.Domain.Entities.Transaction;

public abstract class BaseTransaction : ITransaction
{
    public Guid Id { get; init; }

    public Guid ClientId { get; init; }

    public DateTime DateTime { get; init; }

    public decimal Amount { get; init; }

    public DateTime InsertDateTime { get; init; }
}
