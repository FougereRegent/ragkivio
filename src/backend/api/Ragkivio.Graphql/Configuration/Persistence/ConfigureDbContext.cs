using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ragkivio.Graphql.Options;
using Ragkivio.Persistence;
using Ragkivio.Persistence.User;
using Ragkivio.Domain.User;
using Ragkivio.Application.Common;
using Ragkivio.Persistence.Common;

namespace Ragkivio.Graphql.Configuration.Persistence;

public static class PersistenceConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistence()
        {
            services.AddDbContext<RagkivioContext>(static (services, options) =>
            {
                var optionDatabase = services.GetRequiredService<IOptions<DatabaseOption>>();
                options.UseNpgsql(optionDatabase.Value.ConnectionString, opts => {
                        opts.EnableRetryOnFailure(
                                maxRetryCount: 3,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorCodesToAdd: null
                                );
                        })
                    .UseLowerCaseNamingConvention();
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}