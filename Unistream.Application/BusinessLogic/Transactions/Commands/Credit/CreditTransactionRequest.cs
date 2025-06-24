namespace Unistream.Application.BusinessLogic.Transactions.Commands.Credit;

public sealed record CreditTransactionRequest(Guid Id, Guid ClientId, DateTime DateTime, decimal Amount);
