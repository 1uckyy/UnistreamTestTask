using Unistream.Domain.Entities.Transaction;

namespace Unistream.Domain.Abstractions.Services;

public interface ITransactionService
{
    void Validate(BaseTransaction transaction);
}
