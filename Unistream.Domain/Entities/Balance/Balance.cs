namespace Unistream.Domain.Entities.Balance;

public sealed class Balance
{
    public Guid Id { get; init; }

    public Guid ClientId { get; init; }

    public DateTime DateTime { get; set; }

    public List<Event> Events { get; init; } = new();
}
