namespace Ragkivio.Graphql.Types.Project;

using HotChocolate.Resolvers;
using Ragkivio.Graphql.Middleware;
using Ragkivio.Domain.Project;
using Ragkivio.Application.Project;
using Ragkivio.Application.Project.Dto;

public static class ProjectDeleteMutation
{
    extension(IObjectTypeDescriptor<Mutation> descriptor)
    {
        public IObjectTypeDescriptor<Mutation> DeleteProject()
        {
            descriptor.Field("deleteProject")
                .Argument("projectId", a => a.Type<UuidType>())
                .Use<AuthMiddleware>()
                .Resolve(HandleAsync);

            return descriptor;
        }
    }

    public static async Task<Project> HandleAsync(IResolverContext ctx)
    {
        var deleteProjectUseCase = ctx.Services.GetRequiredService<DeleteProjectUseCase>();
        var id = ctx.ArgumentValue<Guid>("projectId");
        var result = await deleteProjectUseCase.HandleAsync(new DeleteProjectRequest(id), ctx.RequestAborted);

        return result.Value;
    }
}