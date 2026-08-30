using Ragkivio.Graphql.Options;

namespace Ragkivio.Graphql.Configuration.Options;

public static class OptionsConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddOptionPattern()
        {
            services.AddOptions<AuthOption>()
                .BindConfiguration(AuthOption.SectionName);
        services.AddOptions<DatabaseOption>()
                .BindConfiguration(DatabaseOption.SectionName);
            return services;
        }
    }
}