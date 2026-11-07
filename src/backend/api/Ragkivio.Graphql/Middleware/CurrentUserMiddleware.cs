using HotChocolate.Resolvers;
using Ragkivio.Application.User;
using Ragkivio.Application.User.Dto;
using Ragkivio.Application.User.Services;
using Ragkivio.Graphql.Common;
using System.Security.Claims;

namespace Ragkivio.Graphql.Middleware;

internal sealed class CurrentUserMiddleware
{
    private readonly FieldDelegate _next;

    public CurrentUserMiddleware(FieldDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(IMiddlewareContext context)
    {
        var httpContext = context.Services.GetRequiredService<IHttpContextAccessor>().HttpContext;

        if (httpContext?.User.Identity?.IsAuthenticated != true)
        {
            await _next(context);
            return;
        }

        var authId = httpContext.User.FindFirst("sub")?.Value
                  ?? httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(authId))
        {
            await _next(context);
            return;
        }

        var userProvider = context.Services.GetRequiredService<IUserProvider>();
        var createUserUseCase = context.Services.GetRequiredService<CreateUserUseCase>();

        await AddOrGetUser(authId, userProvider, createUserUseCase, context.RequestAborted);

        await _next(context);
    }

    private static async Task AddOrGetUser(string authId, IUserProvider userProvider, CreateUserUseCase userUseCase, CancellationToken token)
    {
        var userResult = await userUseCase.HandleAsync(new CreateUserDto(authId), token);
        userResult.UnwrapOrThrow();

        userProvider.SetCurrentUser(userResult.Value);
    }
}
