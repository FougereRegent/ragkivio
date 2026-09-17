using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.User.Dto;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User;

public sealed class CreateUserUseCase(IUserService userService, IUnitOfWork unitOfWork) : IUseCaseAsync<UserDomain, CreateUserDto>
{
    public async Task<Result<UserDomain>> HandleAsync(CreateUserDto input, CancellationToken token = default)
    {
        return await unitOfWork.ExecuteAsync(async () =>
        {
            return await userService.CreateOrGetUserAsync(input, token);
        }, token);
    }
}