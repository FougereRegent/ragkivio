namespace Ragkivio.Graphql.Types.User;

using HotChocolate.Resolvers;
using Ragkivio.Graphql.Middleware;
using Ragkivio.Application.User;
using Ragkivio.Application.User.Dto;
using Ragkivio.Domain.User;

public static class UserRegisterMutation
{
    extension(IObjectTypeDescriptor<Mutation> descriptor)
    {
        public IObjectTypeDescriptor<Mutation> RegisterUser()
        {
            descriptor.Field("registerUser")
                .Argument("input", a => a.Type<UserRegisterType>())
                .Use<AuthMiddleware>()
                .Resolve(HandleAsync);
            return descriptor;
        }
    }
    private static async Task<User> HandleAsync(IResolverContext ctx) {
        var userRegisterUseCase = ctx.Services.GetRequiredService<RegisterUserUseCase>();
        var input = ctx.ArgumentValue<RegisterUserDto>("input");
        var result = await userRegisterUseCase.HandleAsync(input, ctx.RequestAborted);
        return result.Value;
    }
}