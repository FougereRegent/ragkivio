using FluentResults;
using Ragkivio.Application.Common.Errors;
using Ragkivio.Application.User.Dto;
using Ragkivio.Domain.Common.Exceptions;
using Ragkivio.Domain.User;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User;

public sealed class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Result<UserDomain>> CreateOrGetUserAsync(CreateUserDto createUser, CancellationToken token = default)
    {
        var user = await userRepository.GetUserByAuthIdAsync(createUser.AuthId, token);
        if(user is not null) {
            return user;
        }

        UserDomain createdUser;
        try {
            createdUser = new UserDomain();
        }
        catch (BusinessException ex){
            return Result.Fail(new DomainError(nameof(UserDomain), ex.Message));
        }

        await userRepository.SaveAsync(createdUser, token);
        return createdUser;
    }
}