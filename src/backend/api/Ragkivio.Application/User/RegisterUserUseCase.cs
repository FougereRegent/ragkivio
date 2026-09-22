namespace Ragkivio.Application.User;

using FluentResults;
using Ragkivio.Application.Common;
using Ragkivio.Application.Common.Errors;
using Ragkivio.Application.User.Dto;
using Ragkivio.Application.User.Mapper;
using Ragkivio.Application.User.Services;
using Ragkivio.Domain.Common.Exceptions;
using Ragkivio.Domain.User;

public sealed class RegisterUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserProvider userProvider, UserMapper mapper)
    : IUseCaseAsync<User, RegisterUserDto>
{
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
        }
        catch (Exception ex) when (ex is BusinessException)
        {
            return Result.Fail(new DomainError("user.register", ex.Message));
        }

        var result = await unitOfWork.ExecuteAsync(async () =>
        {
            await userRepository.UpdateAsync(user, token);
            return user;
        }, token);

        return user;
    }
}