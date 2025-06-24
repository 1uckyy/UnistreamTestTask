using Microsoft.EntityFrameworkCore;
using Unistream.Domain.Entities.Balance;
using Unistream.Domain.Entities.Client;
using Unistream.Domain.Entities.Transaction;

namespace Unistream.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Balance> Balances { get; set; }

    public DbSet<BaseTransaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CreditTransaction>();
        modelBuilder.Entity<DebitTransaction>();

        modelBuilder.Ignore<Event>();
        modelBuilder.Ignore<FundsCredited>();
        modelBuilder.Ignore<FundsDebited>();
        modelBuilder.Ignore<TransactionReverted>();

        //AddMockupData(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private void AddMockupData(ModelBuilder modelBuilder)
    {
        var clientId = new Guid("cfaa0d3f-7fea-4423-9f69-ebff826e2f89");
        modelBuilder.Entity<Client>()
            .HasData(new Client { Id = clientId });

        //var balanceId = new Guid("65b26ba4-e5a7-4e82-bc0f-fd3d2da8ea84");
        //var balanceDateTime = new DateTime(2025, 6, 22, 13, 34, 25, DateTimeKind.Utc);

        //modelBuilder.Entity<Balance>()
        //    .HasData(
        //        new Balance
        //        {
        //            Id = balanceId,
        //            ClientId = clientId,
        //            DateTime = balanceDateTime,
        //            Events = new List<Event>()
        //        }
        //    );
    }
}
