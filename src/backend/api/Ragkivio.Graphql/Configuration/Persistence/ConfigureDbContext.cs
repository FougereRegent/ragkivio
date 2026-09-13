using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ragkivio.Graphql.Options;
using Ragkivio.Persistence;
using EFCore.NamingConventions;
using Ragkivio.Persistence.User;
using Ragkivio.Domain.User;

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
                options.UseNpgsql(optionDatabase.Value.ConnectionString)
                    .UseLowerCaseNamingConvention();
            });

            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}