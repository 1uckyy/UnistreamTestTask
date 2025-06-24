using System.Data;

namespace Unistream.Domain.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable);
}
