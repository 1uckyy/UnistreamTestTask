using MediatR;

namespace Unistream.Application.BusinessLogic.Transactions.Commands.Credit;

public sealed record CreditTransactionCommand(Guid Id, Guid ClientId, DateTime DateTime, decimal Amount) : IRequest<CreditTransactionResponse>;
