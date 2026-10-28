namespace Ragkivio.Domain.Common;

public interface IDomainEvent
{
    public DateTimeOffset OccurredOn { get; }
}