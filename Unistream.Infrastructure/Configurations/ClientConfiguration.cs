using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unistream.Domain.Entities.Balance;
using Unistream.Domain.Entities.Client;
using Unistream.Domain.Entities.Transaction;

namespace Unistream.Infrastructure.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder
            .HasOne<Balance>()
            .WithOne()
            .HasForeignKey<Balance>(b => b.ClientId);
        
        builder
            .HasMany<BaseTransaction>()
            .WithOne()
            .HasForeignKey(t => t.ClientId);
    }
}
