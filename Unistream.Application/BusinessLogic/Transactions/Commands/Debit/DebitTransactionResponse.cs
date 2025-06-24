namespace Unistream.Application.BusinessLogic.Transactions.Commands.Debit;

public sealed record DebitTransactionResponse(DateTime InsertDateTime, decimal ClientBalance);
