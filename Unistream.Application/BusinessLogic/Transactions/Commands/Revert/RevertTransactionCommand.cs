using MediatR;

namespace Unistream.Application.BusinessLogic.Transactions.Commands.Revert;

public sealed record RevertTransactionCommand(Guid Id) : IRequest<RevertTransactionResponse>;
