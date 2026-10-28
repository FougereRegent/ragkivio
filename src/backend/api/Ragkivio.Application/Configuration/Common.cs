using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Application.Common;
using Ragkivio.Domain.Common;

namespace Ragkivio.Application.Configuration;

public static class CommonConfiguration
{
    extension(IServiceCollection collection) {
        public IServiceCollection AddCommon() {
            collection.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            return collection;
        }
    }
}