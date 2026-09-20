using HotChocolate.Resolvers;
using Ragkivio.Application.User;
using Ragkivio.Application.User.Dto;
using Ragkivio.Graphql.Middleware;

namespace Ragkivio.Graphql.Types.User;

public static class UserMeQueryCls
{
    extension(IObjectTypeDescriptor<Query> descriptor)
    {
        public IObjectTypeDescriptor<Query> Me()
        {
            descriptor.Field("me")
                .Use<AuthMiddleware>()
                .Resolve(HandleAsync);
            return descriptor;
        }
    }

    private static UserInformationResponse HandleAsync(IResolverContext ctx) {
        var userMeUseCase = ctx.Services.GetRequiredService<MeUserUseCase>();
        var resultUser = userMeUseCase.Handle();
        return resultUser.Value;
    }
}