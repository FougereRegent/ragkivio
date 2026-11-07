namespace Ragkivio.Application.User;

using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.Common.Errors;
using Ragkivio.Application.User.Dto;
using Ragkivio.Application.User.Services;
using Ragkivio.Domain.Common.Exceptions;
using Ragkivio.Domain.User;

public sealed class RegisterUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserProvider userProvider)
    : IUseCaseAsync<User, RegisterUserDto>
{
    private const string resource = "user.register";

    public async Task<Result<User>> HandleAsync(RegisterUserDto input, CancellationToken token = default)
    {
        var user = userProvider.CurrentUser;

        try
        {
            user.RegisterUser(
                    email: input.Email,
                    firstName: input.FirstName,
                    lastName: input.LastName,
                    phoneNumber: input.PhoneNumber,
                    birthDate: input.BirthDate
                    );

            return await unitOfWork.ExecuteAsync(async () =>
            {
                await userRepository.UpdateAsync(user, token);
                return user;
            }, token);
        }
        catch (BusinessException ex)
        {
            return Result.Fail(new DomainError(resource, ex));
        }
        catch (UnitOfWorkSaveException ex)
        {
            return Result.Fail(new InfrastructureError(resource, ex));
        }
    }
}
