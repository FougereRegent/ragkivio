namespace Ragkivio.Graphql.Types.Project;

using HotChocolate.Resolvers;
using Ragkivio.Graphql.Middleware;
using Ragkivio.Domain.Project;
using Ragkivio.Application.Project;
using Ragkivio.Application.Project.Dto;
using Ragkivio.Graphql.Common;

public static class ProjectDeleteMutation
{
    private const string argumentProjectId = "projectId";

    extension(IObjectTypeDescriptor<Mutation> descriptor)
    {
        public IObjectTypeDescriptor<Mutation> DeleteProject()
        {
            descriptor.Field("deleteProject")
                .Argument(argumentProjectId, a => a.Type<UuidType>())
                .Use<AuthMiddleware>()
                .Resolve(HandleAsync);

            return descriptor;
        }
    }

    public static async Task<Project> HandleAsync(IResolverContext ctx)
    {
        var deleteProjectUseCase = ctx.Services.GetRequiredService<DeleteProjectUseCase>();
        var id = ctx.ArgumentValue<Guid>(argumentProjectId);
        var result = await deleteProjectUseCase.HandleAsync(new DeleteProjectRequest(id), ctx.RequestAborted);

        return result.ToGraphQlException();
    }
}