using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ragkivio.Graphql.Types;

namespace Ragkivio.Graphql.Configuration.Presentation;

public static class PresentationConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPresentation()
        {
            services.AddAuthentication(static options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options=>
            {
                options.Authority = "https://markivio.eu.auth0.com/";
                options.Audience = "https://ragkivio-api-dev.damien-venant.ovh";
            });

            services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType<QueryType>()
                //.AddMutationType<MutationType>()
                .AddFiltering()
                .AddSorting();

            services.AddHttpContextAccessor();
            services.AddHealthChecks();
            return services;
        }
    }
}