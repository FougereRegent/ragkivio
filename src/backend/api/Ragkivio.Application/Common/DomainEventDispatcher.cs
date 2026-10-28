using Ragkivio.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Ragkivio.Application.Common;

public sealed class DomainEventDispatcher(IServiceProvider serviceProvider) : IDomainEventDispatcher
{
    public async Task DispatchAsync(IEnumerable<IDomainEvent> @events, 
            CancellationToken cancellationToken = default)
    {
        foreach(var domainEvent in @events) 
        {
            var domainType = domainEvent.GetType();
            var handlerType = typeof(IDomainEventHandler<>)
                .MakeGenericType(domainType);

            var handlers = serviceProvider.GetServices(handlerType);

            foreach(var handler in handlers) {
                var wrapper = HandlerWrapper.Create(handler, domainType);
                await wrapper.HandleAsync(domainEvent, cancellationToken);
            }
        }
    }
}

internal abstract class HandlerWrapper
{
    internal abstract Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

    internal static HandlerWrapper Create(object handler, Type eventType)
    {
        var wrapperType = typeof(HandlerWrapper<>).MakeGenericType(eventType);

        return (HandlerWrapper)Activator.CreateInstance(wrapperType, handler)!;
    }
}

internal sealed class HandlerWrapper<T>(object handler) : HandlerWrapper where T : IDomainEvent
{
    private readonly IDomainEventHandler<T> _handler = (IDomainEventHandler<T>)handler;

    internal override Task HandleAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        => _handler.HandleAsync((T)domainEvent, cancellationToken);
}