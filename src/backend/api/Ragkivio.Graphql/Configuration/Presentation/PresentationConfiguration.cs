using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Ragkivio.Graphql.Options;
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
            .AddJwtBearer();

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IOptions<AuthOption>>((options, auth) =>
                {
                    options.Authority = auth.Value.Authority;
                    options.Audience = auth.Value.Audience;
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
                .ModifyPagingOptions(opts =>
                {
                    opts.MaxPageSize = 100;
                    opts.IncludeTotalCount = true;
                    opts.DefaultPageSize = 25;
                })
                .TryAddTypeInterceptor<AggregateRootTypeInterceptor>()
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