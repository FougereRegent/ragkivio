namespace Ragkivio.Graphql.Types.Project;

using HotChocolate.Resolvers;
using Ragkivio.Graphql.Middleware;
using Ragkivio.Domain.Project;
using Ragkivio.Application.Project;
using Ragkivio.Application.Project.Dto;
using Ragkivio.Graphql.Common;

public static class ProjectCreationMutation
{
    extension(IObjectTypeDescriptor<Mutation> descriptor)
    {
        public IObjectTypeDescriptor<Mutation> CreateProject()
        {
            descriptor.Field("createProject")
                .Argument("input", a => a.Type<CreateProjectRequestType>())
                .Use<CurrentUserMiddleware>()
                .Resolve(HandleAsync);
            return descriptor;
        }
    }

    public static async Task<Project> HandleAsync(IResolverContext ctx)
    {
        var createProjectUseCase = ctx.Services.GetRequiredService<CreateProjectUseCase>();
        var input = ctx.ArgumentValue<CreateProjectRequest>("input");
        var result = await createProjectUseCase.HandleAsync(input, ctx.RequestAborted);

        return result.UnwrapOrThrow();
    }
}