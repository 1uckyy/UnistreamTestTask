namespace Unistream.Application.BusinessLogic.Transactions.Commands.Revert;

public sealed record RevertTransactionResponse(DateTime RevertDateTime, decimal ClientBalance);
