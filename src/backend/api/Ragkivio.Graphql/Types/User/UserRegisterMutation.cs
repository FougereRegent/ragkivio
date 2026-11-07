namespace Ragkivio.Graphql.Types.User;

using HotChocolate.Resolvers;
using Ragkivio.Graphql.Middleware;
using Ragkivio.Application.User;
using Ragkivio.Application.User.Dto;
using Ragkivio.Domain.User;
using Ragkivio.Graphql.Common;

public static class UserRegisterMutation
{
    private const string argumentInput = "input";
    extension(IObjectTypeDescriptor<Mutation> descriptor)
    {
        public IObjectTypeDescriptor<Mutation> RegisterUser()
        {
            descriptor.Field("registerUser")
                .Argument(argumentInput, a => a.Type<UserRegisterType>())
                .Use<CurrentUserMiddleware>()
                .Resolve(HandleAsync);
            return descriptor;
        }
    }
    private static async Task<User> HandleAsync(IResolverContext ctx) {
        var userRegisterUseCase = ctx.Services.GetRequiredService<RegisterUserUseCase>();
        var input = ctx.ArgumentValue<RegisterUserDto>(argumentInput);
        var result = await userRegisterUseCase.HandleAsync(input, ctx.RequestAborted);
        return result.ToGraphQlException();
    }
}