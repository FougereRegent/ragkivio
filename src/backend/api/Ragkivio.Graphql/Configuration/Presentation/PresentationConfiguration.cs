using Ragkivio.Graphql.Types;

namespace Ragkivio.Graphql.Configuration.Presentation;

public static class PresentationConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentation()
        {
            services.AddGraphQLServer()
                .AddQueryType<QueryType>()
                //.AddMutationType<MutationType>()
                .AddFiltering()
                .AddSorting();

            services.AddHealthChecks();
            return services;
        }
    }
}