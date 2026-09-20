using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.User.Services;

namespace Ragkivio.Application.User;
using Dto;

public sealed class MeUserUseCase(IUserProvider userProvider) : IUseCase<UserInformationResponse>
{
    public Result<UserInformationResponse> Handle()
    {
        var currentUser = userProvider.CurrentUser;
        return new UserInformationResponse {
            Id = currentUser.Id,
            Email = currentUser.Email,
            FirstName = currentUser.FirstName,
            LastName = currentUser.LastName,
        };
    }
}