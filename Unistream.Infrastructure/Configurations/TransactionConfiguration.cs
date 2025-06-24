using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unistream.Domain.Entities.Client;
using Unistream.Domain.Entities.Transaction;

namespace Unistream.Infrastructure.Configurations;

internal sealed class TransactionConfiguration : IEntityTypeConfiguration<BaseTransaction>
{
    public void Configure(EntityTypeBuilder<BaseTransaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder
            .HasOne<Client>()
            .WithMany()
            .HasForeignKey(t => t.ClientId);

        builder
            .Property(x => x.InsertDateTime)
            .IsRequired()
            .HasDefaultValueSql("NOW()");
    }
}
