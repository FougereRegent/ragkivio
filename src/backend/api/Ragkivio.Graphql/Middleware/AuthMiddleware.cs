using HotChocolate.Resolvers;
using System.IdentityModel.Tokens.Jwt;

namespace Ragkivio.Graphql.Middleware;

internal sealed class AuthMiddleware {

    private readonly FieldDelegate _next;

    public AuthMiddleware(FieldDelegate next) {
        _next = next;
    }

    readonly record struct UserId(string AuthId);

    public async Task InvokeAsync(IMiddlewareContext context) {
        const string prefixAuth = "Bearer ";
        var httpContext = context.RequestServices.GetRequiredService<IHttpContextAccessor>()
            .HttpContext;

        if(httpContext is null) {
            await _next(context);
            return;
        }

        var auth = httpContext?.Request.Headers.Authorization.FirstOrDefault();

        if(auth is not null && auth.StartsWith(prefixAuth, StringComparison.OrdinalIgnoreCase)) {
            var jwt =  auth[prefixAuth.Length..].Trim();
            var userObject = ParseJwt(jwt);

            await _next(context);
        }
        else {

        }
    }

    private async Task AddOrGetUser() {

    }

    private UserId ParseJwt(string jwt) {
        var securityToken = new JwtSecurityTokenHandler();
        var tok = securityToken.ReadJwtToken(jwt);

        return new UserId(
                AuthId: tok.Subject
                );
    }
}