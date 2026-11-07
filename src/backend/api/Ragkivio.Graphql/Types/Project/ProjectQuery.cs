using Ragkivio.Application.Project.Services;
using Ragkivio.Graphql.Middleware;

namespace Ragkivio.Graphql.Types.Project;

public static class ProjectQuery
{
    extension(IObjectTypeDescriptor<Query> descriptor)
    {
        public IObjectTypeDescriptor<Query> Projects()
        {
            descriptor.Field("projects")
                .Use<CurrentUserMiddleware>()
                .UsePaging()
                .UseFiltering()
                .UseSorting()
                .Resolve(ctx =>
                {
                    var projectService = ctx.Services.GetRequiredService<IProjectService>();
                    return projectService.GetAll();
                })
                .Type<ListType<ProjectType>>();

            return descriptor;
        }
    }
}