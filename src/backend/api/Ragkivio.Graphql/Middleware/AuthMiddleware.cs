using FluentResults;
using HotChocolate.Resolvers;
using Ragkivio.Application.User;
using Ragkivio.Application.User.Dto;
using Ragkivio.Application.User.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Ragkivio.Graphql.Middleware;

internal sealed class AuthMiddleware
{

    private readonly FieldDelegate _next;

    public AuthMiddleware(FieldDelegate next)
    {
        _next = next;
    }

    readonly record struct UserId(string AuthId);

    public async Task InvokeAsync(IMiddlewareContext context)
    {
        const string prefixAuth = "Bearer ";
        var httpContext = context.RequestServices.GetRequiredService<IHttpContextAccessor>()
            .HttpContext;

        var userProvider = context.RequestServices.GetRequiredService<IUserProvider>();
        var userService = context.RequestServices.GetRequiredService<IUserService>();

        if (httpContext is null)
        {
            await _next(context);
            return;
        }

        var auth = httpContext?.Request.Headers.Authorization.FirstOrDefault();

        if (auth is not null && auth.StartsWith(prefixAuth, StringComparison.OrdinalIgnoreCase))
        {
            var jwt = auth[prefixAuth.Length..].Trim();
            var userObject = ParseJwt(jwt);

            var result = await AddOrGetUser(userObject, userProvider, userService);
            if (result.IsFailed)
            {
                throw new GraphQLException(
                        ErrorBuilder.New()
                        .SetMessage(result.Errors[0].Message)
                        .SetCode("AUTH")
                        .Build()
                        );
            }
            await _next(context);
        }
        else
        {

        }
    }

    private static async Task<Result> AddOrGetUser(UserId user, IUserProvider userProvider, IUserService userService, CancellationToken token = default)
    {
        var userResult = await userService.CreateOrGetUserAsync(new CreateUserDto(user.AuthId), token);
        if (userResult.IsFailed)
        {
            return Result.Fail("");
        }
        userProvider.SetCurrentUser(userResult.Value);

        return Result.Ok();
    }

    private UserId ParseJwt(string jwt)
    {
        var securityToken = new JwtSecurityTokenHandler();
        var tok = securityToken.ReadJwtToken(jwt);

        return new UserId(
                AuthId: tok.Subject
                );
    }
}