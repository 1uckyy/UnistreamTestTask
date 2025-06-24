using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Unistream.Domain.Abstractions.Repositories;

namespace Unistream.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<IDbTransaction> BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(isolationLevel);

        return transaction.GetDbTransaction();
    }
}
