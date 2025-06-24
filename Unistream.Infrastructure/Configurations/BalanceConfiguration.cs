using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unistream.Domain.Entities.Balance;
using Unistream.Domain.Entities.Client;

namespace Unistream.Infrastructure.Configurations;

internal sealed class BalanceConfiguration : IEntityTypeConfiguration<Balance>
{
    public void Configure(EntityTypeBuilder<Balance> builder)
    {
        builder.HasKey(b => b.Id);

        builder
            .HasOne<Client>()
            .WithOne()
            .HasForeignKey<Balance>(b => b.ClientId);

        builder
            .Property(b => b.Events)
            .HasColumnType("jsonb");
    }
}
