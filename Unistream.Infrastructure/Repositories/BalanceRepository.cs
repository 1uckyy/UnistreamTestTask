using Microsoft.EntityFrameworkCore;
using Unistream.Domain.Abstractions.Repositories;
using Unistream.Domain.Entities.Balance;

namespace Unistream.Infrastructure.Repositories;

public sealed class BalanceRepository : IBalanceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BalanceRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task<Balance?> GetByClientId(Guid clientId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Balances
            .AsNoTracking()
            .FirstOrDefaultAsync(balance => balance.ClientId == clientId, cancellationToken);
    }

    public async Task UpdateEvents(Guid id, List<Event> events, CancellationToken cancellationToken = default)
    {
        await _dbContext.Balances
            .Where(b => b.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Events, events)
                .SetProperty(b => b.DateTime, DateTime.UtcNow),
                cancellationToken
            );
    }
}
