using Ragkivio.Graphql.Configuration.Application;
using Ragkivio.Graphql.Configuration.Infrastructure;
using Ragkivio.Graphql.Configuration.Options;
using Ragkivio.Graphql.Configuration.Presentation;
using Ragkivio.Persistence.Configuration;

namespace Ragkivio.Graphql.Configuration;

public static class ConfigurationExt
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddConfig()
        {
            services
                .AddOptionPattern()
                .AddPersistence()
                .AddInfrastructure()
                .AddApplication()
                .AddPresentation();
            return services;
        }
    }

}