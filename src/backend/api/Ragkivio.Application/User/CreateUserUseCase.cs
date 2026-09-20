using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.User.Dto;
using Ragkivio.Application.User.Services;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User;

public sealed class CreateUserUseCase(IUserService userService, 
    IUnitOfWork unitOfWork,
    IUserProvider userProvider) : IUseCaseAsync<UserDomain, CreateUserDto>
{
    public async Task<Result<UserDomain>> HandleAsync(CreateUserDto input, CancellationToken token = default)
    {
        var result =  await unitOfWork.ExecuteAsync(async () =>
        {
            return await userService.CreateOrGetUserAsync(input, token);
        }, token);
        
        userProvider.SetCurrentUser(result.Value);
        return result;
    }
}