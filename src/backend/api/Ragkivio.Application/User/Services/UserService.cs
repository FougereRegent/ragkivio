using FluentResults;
using Ragkivio.Application.Common.Errors;

namespace Ragkivio.Application.User.Services;

public sealed class UserService(IUserProvider userProvider) : IUserService
{
    public Result<bool> UserRegistrationIsCompleted()
    {
        if(userProvider.CurrentUser is null) {
            return Result.Fail(new NotFoundError("user", "", "current user not found"));
        }
        return userProvider.CurrentUser.IsRegistered;
    }
}