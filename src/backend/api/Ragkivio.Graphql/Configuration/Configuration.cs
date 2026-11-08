namespace Ragkivio.Graphql.Configuration;

using Ragkivio.Application.Configuration;
using Ragkivio.Graphql.Configuration.Options;
using Ragkivio.Graphql.Configuration.Presentation;
using Ragkivio.Persistence.Configuration;
using Ragkivio.Infrastructure.Configuration;

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