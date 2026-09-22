using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.User.Services;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User;

public sealed class MeUserUseCase(IUserProvider userProvider) : IUseCase<UserDomain>
{
    public Result<UserDomain> Handle()
    {
        var currentUser = userProvider.CurrentUser;
        return currentUser;
    }
}