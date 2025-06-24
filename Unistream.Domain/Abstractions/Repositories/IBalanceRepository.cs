using Unistream.Domain.Entities.Balance;

namespace Unistream.Domain.Abstractions.Repositories;

public interface IBalanceRepository
{
    Task<Balance?> GetByClientId(Guid clientId, CancellationToken cancellationToken = default);

    Task UpdateEvents(Guid id, List<Event> events, CancellationToken cancellationToken = default);
}
