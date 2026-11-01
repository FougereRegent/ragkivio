namespace Ragkivio.Persistence.Configuration.Project;

using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Domain.Project;
using Ragkivio.Persistence.Project;

public static class ProjectConfiguration
{
    extension(IServiceCollection serviceCollection)
    {
        public IServiceCollection AddProjectPersistence()
        {
            serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();
            return serviceCollection;
        }
    }
}