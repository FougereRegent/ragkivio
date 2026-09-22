using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.Common.Errors;
using Ragkivio.Application.User.Dto;
using Ragkivio.Application.User.Services;
using Ragkivio.Domain.Common.Exceptions;
using Ragkivio.Domain.User;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User;

public sealed class CreateUserUseCase(IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IUserProvider userProvider) : IUseCaseAsync<UserDomain, CreateUserDto>
{
    public async Task<Result<UserDomain>> HandleAsync(CreateUserDto input, CancellationToken token = default)
    {
        var user = await userRepository.GetUserByAuthIdAsync(input.AuthId, token);
        if (user is not null)
        {
            return user;
        }

        UserDomain createdUser;
        try
        {
            createdUser = new UserDomain();
        }
        catch (BusinessException ex)
        {
            return Result.Fail(new DomainError(nameof(UserDomain), ex.Message));
        }

        var result = await unitOfWork.ExecuteAsync(async () =>
        {
            return await userRepository.SaveAsync(createdUser, input.AuthId, token) ?? createdUser;
        }, token);

        userProvider.SetCurrentUser(result);
        return result;
    }
}