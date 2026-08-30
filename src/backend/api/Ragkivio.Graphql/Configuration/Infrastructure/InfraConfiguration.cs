namespace Ragkivio.Graphql.Configuration.Infrastructure;

public static class InfrastructureConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddInfrastructure()
        {
            return services;
        }
    }
}