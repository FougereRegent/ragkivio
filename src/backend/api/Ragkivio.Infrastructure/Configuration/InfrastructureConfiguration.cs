using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Infrastructure.Configuration.FileStorage;

namespace Ragkivio.Infrastructure.Configuration;

public static class InfrastructureConfiguration
{
    extension(IServiceCollection serviceCollection)
    {
        public IServiceCollection AddInfrastructure() =>
            serviceCollection.AddFileStorage();
    }
}