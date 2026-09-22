
using Ragkivio.Application.User.Dto;
namespace Ragkivio.Graphql.Types.User;

public sealed class UserRegisterType : InputObjectType<RegisterUserDto>
{
    protected override void Configure(IInputObjectTypeDescriptor<RegisterUserDto> descriptor)
    {
        descriptor.Field(f => f.Email)
            .Type<NonNullType<StringType>>();
        descriptor.Field(f => f.FirstName)
            .Type<NonNullType<StringType>>();
        descriptor.Field(f => f.LastName)
            .Type<NonNullType<StringType>>();
        descriptor.Field(f => f.BirthDate)
            .Type<NonNullType<LocalDateType>>();
        descriptor.Field(f => f.PhoneNumber)
            .Type<StringType>()
            .DefaultValue(null);
    }
}