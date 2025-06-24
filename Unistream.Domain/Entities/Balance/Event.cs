using System.Text.Json.Serialization;

namespace Unistream.Domain.Entities.Balance;

[JsonDerivedType(typeof(FundsCredited), typeDiscriminator: "FundsCredited")]
[JsonDerivedType(typeof(FundsDebited), typeDiscriminator: "FundsDebited")]
[JsonDerivedType(typeof(TransactionReverted), typeDiscriminator: "TransactionReverted")]
public abstract record Event
{
    public DateTime Timestamp { get; init; } = DateTime.Now;
}

public record FundsCredited(
    decimal Amount,
    Guid TransactionId
) : Event;

public record FundsDebited(
    decimal Amount,
    Guid TransactionId
) : Event;

public record TransactionReverted(
    Guid RevertedTransactionId
) : Event;
