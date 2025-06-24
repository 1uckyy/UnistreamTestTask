namespace Unistream.Application.BusinessLogic.Balances.Queries.GetByClientId;

public sealed record BalanceResponse(DateTime BalanceDateTime, decimal ClientBalance);
