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
            .AddJwtBearer(options =>
            {
                options.Authority = "https://ragkivio.eu.auth0.com/";
                options.Audience = "https://graphql-ragkivio.fr";
            });

            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAllOrigin", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType<QueryType>()
                .AddMutationType<MutationType>()
                .AddFiltering()
                .AddSorting();

            services.AddHttpContextAccessor();
            services.AddHealthChecks();
            return services;
        }
    }
}