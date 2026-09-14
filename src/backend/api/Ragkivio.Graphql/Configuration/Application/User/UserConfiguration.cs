using Ragkivio.Application.User;
using Ragkivio.Application.User.Services;

namespace Ragkivio.Graphql.Configuration.Application.User;

public static class UserConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddUserApplication() 
        {
            services.AddScoped<IUserProvider, UserProvider>()
                .AddScoped<IUserService, UserService>()
                .AddTransient<RegisterUserUseCase>();
            return services;
        }
    }
}