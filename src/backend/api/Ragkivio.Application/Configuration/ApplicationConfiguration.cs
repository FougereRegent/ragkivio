namespace Ragkivio.Application.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Application.Configuration.User;
using Ragkivio.Application.Configuration.Project;
using Ragkivio.Application.Configuration.Application;

public static class ApplicationConfiguration
{
    extension(IServiceCollection collection)
    {
        public IServiceCollection AddApplication()
        {
            collection.AddCommon()
                .AddUser()
                .AddProject()
                .AddDocument();

            return collection;
        }
    }
}