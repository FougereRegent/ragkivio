namespace Ragkivio.Graphql.Types.User;

using Ragkivio.Domain.User;
using Ragkivio.Application.User.Dto;

public sealed class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(f => f.Id)
            .Type<NonNullType<UuidType>>();
        descriptor.Field(f => f.Email)
            .Type<NonNullType<StringType>>();

        descriptor.Field(f => f.FirstName)
            .Type<NonNullType<StringType>>();

        descriptor.Field(f => f.LastName)
            .Type<NonNullType<StringType>>();

        descriptor.Field("registrationState")
            .Resolve(context =>
            {
                var user = context.Parent<User>();
                return user.IsRegistered switch {
                    true => RegistrationState.Completed,
                    false => RegistrationState.NotCompleted
                };
            })
            .Type<EnumType<RegistrationState>>();
    }
}