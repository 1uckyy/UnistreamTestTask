using MediatR;
using Unistream.Domain.Abstractions.Repositories;
using Unistream.Domain.Abstractions.Services;
using Unistream.Domain.Entities.Balance;
using Unistream.Domain.Exceptions;

namespace Unistream.Application.BusinessLogic.Transactions.Commands.Revert;

public class RevertTransactionCommandHandler : IRequestHandler<RevertTransactionCommand, RevertTransactionResponse>
{
    private readonly IBalanceService _balanceService;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBalanceRepository _balanceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RevertTransactionCommandHandler(
        IBalanceService balanceService,
        ITransactionRepository transactionRepository,
        IBalanceRepository balanceRepository,
        IUnitOfWork unitOfWork
    )
    {
        _balanceService = balanceService;
        _transactionRepository = transactionRepository;
        _balanceRepository = balanceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RevertTransactionResponse> Handle(RevertTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetById(request.Id, cancellationToken);

        if (transaction is null) throw new TransactionNotFoundException(request.Id);

        using var transactionScope = await _unitOfWork.BeginTransaction();

        try
        {
            var balance = await _balanceRepository.GetByClientId(transaction.ClientId, cancellationToken);

            if (balance is null) throw new BalanceNotFoundException(transaction.ClientId);

            if (_balanceService.IsTransactionAlreadyReverted(balance, transaction.Id, out var @event))
                return new RevertTransactionResponse(@event!.Timestamp, _balanceService.GetAmount(balance));

            @event = new TransactionReverted(transaction.Id);
            balance.Events.Add(@event);

            if (_balanceService.GetAmount(balance) < 0)
                throw new NotEnoughFundsException(balance.ClientId);

            await _balanceRepository.UpdateEvents(balance.Id, balance.Events, cancellationToken);

            transactionScope.Commit();

            return new RevertTransactionResponse(@event.Timestamp, _balanceService.GetAmount(balance));
        }
        catch
        {
            transactionScope.Rollback();
            throw;
        }
    }
}
