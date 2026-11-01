namespace Ragkivio.Application.Project.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Ragkivio.Application.Project.Services;

public static class ProjectConfiguration
{
    extension(IServiceCollection serviceColelction)
    {
        public IServiceCollection AddProject()
        {
            serviceColelction.AddScoped<IProjectService, ProjectService>()
                .AddTransient<CreateProjectUseCase>();

            return serviceColelction;
        }
    }
}