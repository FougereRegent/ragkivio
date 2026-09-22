using Ragkivio.Application.User;
using Ragkivio.Application.User.Services;
using Ragkivio.Application.User.Mapper;

namespace Ragkivio.Graphql.Configuration.Application.User;

public static class UserConfiguration
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddUserApplication() 
        {
            services.AddScoped<IUserProvider, UserProvider>()
                .AddScoped<IUserService, UserService>()
                .AddSingleton<UserMapper>()
                .AddTransient<RegisterUserUseCase>()
                .AddTransient<CreateUserUseCase>()
                .AddTransient<MeUserUseCase>();

            return services;
        }
    }
}