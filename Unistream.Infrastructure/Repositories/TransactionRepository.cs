using Microsoft.EntityFrameworkCore;
using Unistream.Domain.Abstractions.Repositories;
using Unistream.Domain.Entities.Transaction;

namespace Unistream.Infrastructure.Repositories;

public sealed class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<BaseTransaction?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Transactions
            .AsNoTracking()
            .FirstOrDefaultAsync(transaction => transaction.Id == id, cancellationToken);
    }

    public async Task Create(BaseTransaction transaction, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(transaction, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
