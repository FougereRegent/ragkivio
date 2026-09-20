using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ragkivio.Application.Common;
using Ragkivio.Persistence.Common;
using Ragkivio.Persistence.Common.Options;
using Microsoft.EntityFrameworkCore;
using Ragkivio.Graphql.Configuration.Persistence.User;

namespace Ragkivio.Persistence.Configuration;

public static class PersistenceConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistence()
        {
            services.AddDbContext<RagkivioContext>(static (services, options) =>
            {
                var optionDatabase = services.GetRequiredService<IOptions<DatabaseOption>>();
                options.UseNpgsql(optionDatabase.Value.ConnectionString, opts =>
                {
                    opts.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorCodesToAdd: null
                            );
                })
                    .UseLowerCaseNamingConvention();
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>()
                .AddUserPersistence();
            return services;
        }

        public IServiceCollection AddPersistenceOption()
        {

            services.AddOptions<DatabaseOption>()
                .BindConfiguration(DatabaseOption.SectionName)
                .ValidateOnStart();

            return services;
        }
    }
}