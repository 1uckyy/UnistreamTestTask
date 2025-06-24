using Unistream.Domain.Entities.Transaction;

namespace Unistream.Domain.Abstractions.Repositories;

public interface ITransactionRepository
{
    Task<BaseTransaction?> GetById(Guid id, CancellationToken cancellationToken = default);

    Task Create(BaseTransaction transaction, CancellationToken cancellationToken = default);
}
