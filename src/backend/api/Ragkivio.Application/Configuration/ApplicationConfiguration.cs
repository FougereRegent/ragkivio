using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Application.Configuration.User;

namespace Ragkivio.Application.Configuration;

public static class ApplicationConfiguration
{
    extension(IServiceCollection collection)
    {
        public IServiceCollection AddApplication()
        {
            collection.AddCommon()
                .AddUser();

            return collection;
        }
    }
}