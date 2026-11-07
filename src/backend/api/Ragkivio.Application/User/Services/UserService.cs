using FluentResults;
using Ragkivio.Application.Common.Errors;

namespace Ragkivio.Application.User.Services;

public sealed class UserService(IUserProvider userProvider) : IUserService
{
    private const string resource = "user.is-registered";

    public Result<bool> UserRegistrationIsCompleted()
    {
        if(userProvider.CurrentUser is null) {
            return Result.Fail(new NotFoundError(resource, ""));
        }
        return userProvider.CurrentUser.IsRegistered;
    }
}