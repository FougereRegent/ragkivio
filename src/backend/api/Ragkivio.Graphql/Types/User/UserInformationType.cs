using Ragkivio.Application.User.Dto;
namespace Ragkivio.Graphql.Types.User;

public sealed class UserInformationType : ObjectType<UserInformationResponse>
{
    protected override void Configure(IObjectTypeDescriptor<UserInformationResponse> descriptor)
    {
        descriptor.Field(f => f.Id)
            .Type<NonNullType<UuidType>>();

        descriptor.Field(f => f.Email)
            .Type<NonNullType<StringType>>();

        descriptor.Field(f => f.FirstName)
            .Type<NonNullType<StringType>>();

        descriptor.Field(f => f.LastName)
            .Type<NonNullType<StringType>>();
    }
}