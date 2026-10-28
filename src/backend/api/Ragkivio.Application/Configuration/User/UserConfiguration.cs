using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Application.User;
using Ragkivio.Application.User.Mapper;
using Ragkivio.Application.User.Services;

namespace Ragkivio.Application.Configuration.User;

public static class UserConfiguration
{
    extension(IServiceCollection collection)
    {
        public IServiceCollection AddUser()
        {
            collection.AddScoped<IUserProvider, UserProvider>()
                .AddScoped<IUserService, UserService>()
                .AddSingleton<UserMapper>()
                .AddTransient<RegisterUserUseCase>()
                .AddTransient<CreateUserUseCase>()
                .AddTransient<MeUserUseCase>();

            return collection;
        }
    }
}