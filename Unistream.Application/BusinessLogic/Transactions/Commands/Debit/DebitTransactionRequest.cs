namespace Unistream.Application.BusinessLogic.Transactions.Commands.Debit;

public sealed record DebitTransactionRequest(Guid Id, Guid ClientId, DateTime DateTime, decimal Amount);
