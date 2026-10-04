using HotChocolate.Resolvers;
using Ragkivio.Application.User;
using Ragkivio.Graphql.Middleware;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Graphql.Types.User;

public static class UserMeQueryCls
{
    extension(IObjectTypeDescriptor<Query> descriptor)
    {
        public IObjectTypeDescriptor<Query> Me()
        {
            descriptor.Field("me")
                .Use<AuthMiddleware>()
                .Resolve(HandleAsync)
                .Type<UserType>();
            return descriptor;
        }
    }

    private static UserDomain HandleAsync(IResolverContext ctx) {
        var userMeUseCase = ctx.Services.GetRequiredService<MeUserUseCase>();
        var resultUser = userMeUseCase.Handle();
        return resultUser.Value;
    }
}