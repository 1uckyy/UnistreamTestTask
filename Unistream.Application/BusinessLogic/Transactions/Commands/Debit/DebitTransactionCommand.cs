using MediatR;

namespace Unistream.Application.BusinessLogic.Transactions.Commands.Debit;

public sealed record DebitTransactionCommand(Guid Id, Guid ClientId, DateTime DateTime, decimal Amount) : IRequest<DebitTransactionResponse>;
