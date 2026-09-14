using Ragkivio.Graphql.Configuration.Application.User;

namespace Ragkivio.Graphql.Configuration.Application;

public static class ApplicationConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            services.AddUserApplication();
            return services;
        }
    }
}