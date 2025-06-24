using MediatR;

namespace Unistream.Application.BusinessLogic.Balances.Queries.GetByClientId;

public sealed record GetBalanceByClientIdQuery(Guid ClientId) : IRequest<BalanceResponse>;
