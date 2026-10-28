namespace Ragkivio.Domain.Common;

public class AggregateRoot 
{
    private readonly List<IDomainEvent> @events = 
        new List<IDomainEvent>();

    public IReadOnlyList<IDomainEvent> DomainEvents => @events;


    public void AddDomainEvent(IDomainEvent domainEvent) {
        @events.Add(domainEvent);
    }

    public void AddDomainEvent(params IDomainEvent[] domainEvents) {
        @events.AddRange(domainEvents);
    }

    public void ClearEvents() => @events.Clear();
}