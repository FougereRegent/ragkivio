using Ragkivio.Graphql.Options;
using Ragkivio.Persistence.Configuration;

namespace Ragkivio.Graphql.Configuration.Options;

public static class OptionsConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddOptionPattern()
        {
            services.AddOptions<AuthOption>()
                .BindConfiguration(AuthOption.SectionName);
            services.AddPersistenceOption();
            return services;
        }
    }
}