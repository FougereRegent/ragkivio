using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ragkivio.Graphql.Options;
using Ragkivio.Persistence;

namespace Ragkivio.Graphql.Configuration.Persistence;

public static class PersistenceConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistence(IConfiguration config)
        {
            services.AddDbContext<RagkivioContext>(static (services, options) =>
            {
                var optionDatabase = services.GetRequiredService<IOptions<DatabaseOption>>();
                options.UseNpgsql(optionDatabase.Value.ConnectionString);
            });
            return services;
        }
    }
}