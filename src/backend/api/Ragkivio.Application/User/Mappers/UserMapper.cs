using Ragkivio.Application.User.Dto;
using Riok.Mapperly.Abstractions;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User.Mapper;

[Mapper]
public partial class UserMapper
{

    [MapperIgnoreTarget(nameof(UserDomain.Id))]
    [MapperIgnoreTarget(nameof(UserDomain.CreatedAt))]
    [MapperIgnoreTarget(nameof(UserDomain.UpdatedAt))]
    public partial UserDomain Map(RegisterUserDto registerUserDto);

    [MapperIgnoreTarget(nameof(UserDomain.Id))]
    [MapperIgnoreTarget(nameof(UserDomain.CreatedAt))]
    [MapperIgnoreTarget(nameof(UserDomain.UpdatedAt))]
    public partial void Hydrate(RegisterUserDto source, UserDomain target);

    private static RegistrationState MapRegistration(bool isRegistered) =>
        isRegistered ? RegistrationState.Completed : RegistrationState.NotCompleted;
}