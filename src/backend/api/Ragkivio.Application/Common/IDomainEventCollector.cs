using Ragkivio.Domain.Common;

namespace Ragkivio.Application.Configuration;

public interface IDomainEventCollector
{
    void Track(params AggregateRoot[] aggregateRoots);
    IReadOnlyList<IDomainEvent> Drain();
    void Discard();
}

internal sealed class DomainEventCollector : IDomainEventCollector
{
    private readonly HashSet<AggregateRoot> _aggregates =
        new(ReferenceEqualityComparer.Instance);

    public void Track(params AggregateRoot[] aggregateRoots)
    {
        foreach (var aggregateRoot in aggregateRoots)
            _aggregates.Add(aggregateRoot);
    }

    public IReadOnlyList<IDomainEvent> Drain()
    {
        // Matérialiser AVANT de vider les agrégats : SelectMany est paresseux.
        var events = _aggregates
            .SelectMany(aggregate => aggregate.DomainEvents)
            .ToList();

        foreach (var aggregate in _aggregates)
            aggregate.ClearEvents();

        _aggregates.Clear();
        return events;
    }

    public void Discard()
    {
        foreach (var aggregate in _aggregates)
            aggregate.ClearEvents();

        _aggregates.Clear();
    }
}