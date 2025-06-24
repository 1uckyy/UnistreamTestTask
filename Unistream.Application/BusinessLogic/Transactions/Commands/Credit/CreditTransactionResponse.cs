namespace Unistream.Application.BusinessLogic.Transactions.Commands.Credit;

public sealed record CreditTransactionResponse(DateTime InsertDateTime, decimal ClientBalance);
