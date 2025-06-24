using MediatR;
using Unistream.Domain.Abstractions.Repositories;
using Unistream.Domain.Abstractions.Services;
using Unistream.Domain.Entities.Balance;
using Unistream.Domain.Entities.Transaction;
using Unistream.Domain.Exceptions;

namespace Unistream.Application.BusinessLogic.Transactions.Commands.Credit;

public class CreditTransactionCommandHandler : IRequestHandler<CreditTransactionCommand, CreditTransactionResponse>
{
    private readonly IBalanceRepository _balanceRepository;
    private readonly IBalanceService _balanceService;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionService _transactionService;
    private readonly IUnitOfWork _unitOfWork;

    public CreditTransactionCommandHandler(
        IBalanceRepository balanceRepository,
        IBalanceService balanceService,
        ITransactionRepository transactionRepository,
        ITransactionService transactionService,
        IUnitOfWork unitOfWork
    )
    {
        _balanceRepository = balanceRepository;
        _balanceService = balanceService;
        _transactionRepository = transactionRepository;
        _transactionService = transactionService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreditTransactionResponse> Handle(CreditTransactionCommand request, CancellationToken cancellationToken)
    {
        var newTransaction = new CreditTransaction
        {
            Id = request.Id,
            ClientId = request.ClientId,
            DateTime = request.DateTime,
            Amount = request.Amount
        };

        _transactionService.Validate(newTransaction);

        using var transactionScope = await _unitOfWork.BeginTransaction();

        try
        {
            var balance = await _balanceRepository.GetByClientId(request.ClientId, cancellationToken);

            if (balance is null) throw new BalanceNotFoundException(request.ClientId);

            var existsTransaction = await _transactionRepository.GetById(request.Id, cancellationToken);

            if (existsTransaction is not null)
                return new CreditTransactionResponse(existsTransaction.InsertDateTime, _balanceService.GetAmount(balance));

            var @event = new FundsCredited(request.Amount, request.Id);
            balance.Events.Add(@event);

            await _balanceRepository.UpdateEvents(balance.Id, balance.Events, cancellationToken);

            await _transactionRepository.Create(newTransaction, cancellationToken);

            transactionScope.Commit();

            return new CreditTransactionResponse(newTransaction.InsertDateTime, _balanceService.GetAmount(balance));
        }
        catch
        {
            transactionScope.Rollback();
            throw;
        }
    }
}
