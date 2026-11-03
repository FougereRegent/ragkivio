namespace Ragkivio.Graphql.Types.Project;

using HotChocolate.Resolvers;
using Ragkivio.Graphql.Middleware;
using Ragkivio.Domain.Project;
using Ragkivio.Application.Project;
using Ragkivio.Application.Project.Dto;

public static class ProjectUpdateMutation
{
    extension(IObjectTypeDescriptor<Mutation> descriptor)
    {
        public IObjectTypeDescriptor<Mutation> UpdateProject()
        {
            descriptor.Field("updateProject")
                .Argument("input", a => a.Type<UpdateProjectRequestType>())
                .Use<AuthMiddleware>()
                .Resolve(HandleAsync);

            return descriptor;
        }
    }

    public static async Task<Project> HandleAsync(IResolverContext ctx)
    {
        var updateProjectUseCase = ctx.Services.GetRequiredService<UpdateProjectUseCase>();
        var input = ctx.ArgumentValue<UpdateProjectRequest>("input");
        var result = await updateProjectUseCase.HandleAsync(input, ctx.RequestAborted);

        return result.Value;
    }
}