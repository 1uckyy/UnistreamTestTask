using MediatR;
using Unistream.Domain.Abstractions.Repositories;
using Unistream.Domain.Abstractions.Services;
using Unistream.Domain.Exceptions;

namespace Unistream.Application.BusinessLogic.Balances.Queries.GetByClientId;

internal sealed class GetBalanceByClientIdQueryHandler : IRequestHandler<GetBalanceByClientIdQuery, BalanceResponse>
{
    private readonly IBalanceService _balanceService;
    private readonly IBalanceRepository _balanceRepository;

    public GetBalanceByClientIdQueryHandler(
        IBalanceService balanceService,
        IBalanceRepository balanceRepository
    )
    {
        _balanceService = balanceService;
        _balanceRepository = balanceRepository;
    }

    public async Task<BalanceResponse> Handle(GetBalanceByClientIdQuery request, CancellationToken cancellationToken)
    {
        var balance = await _balanceRepository.GetByClientId(request.ClientId, cancellationToken);

        if (balance is null) throw new BalanceNotFoundException(request.ClientId);

        return new BalanceResponse(balance.DateTime, _balanceService.GetAmount(balance));
    }
}
