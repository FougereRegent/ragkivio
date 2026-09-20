using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Domain.User;
using Ragkivio.Persistence.User;

namespace Ragkivio.Graphql.Configuration.Persistence.User;

public static class UserPersistenceConfiguration {
    extension(IServiceCollection services)  {
        public IServiceCollection AddUserPersistence() {
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }

}