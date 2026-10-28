namespace Ragkivio.Domain.Common;

public interface IDomainEventHandler<in T> where T : IDomainEvent 
{
    public Task HandleAsync(T @event, CancellationToken cancellationToken = default);
}