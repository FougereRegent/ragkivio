using FluentResults;
using Ragkivio.Application.User.Dto;
using Ragkivio.Domain.User;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User;

public sealed class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<Result<UserDomain>> CreateUserAsync(CreateUserDto createUser, CancellationToken token = default)
    {
        var user = userRepository.GetUserByAuthIdAsync(createUser.AuthId, token);
        if(user is not null) {
            return Result.Fail();
        }
    }
}