namespace Ragkivio.Graphql.Types.Project;

using Ragkivio.Application.Project.Dto;
using Ragkivio.Domain.Project;

public sealed class ProjectType : ObjectType<Project>
{
    protected override void Configure(IObjectTypeDescriptor<Project> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>();

        descriptor.Field(f => f.Description)
            .Type<StringType>();

        descriptor.Field(f => f.Visibility)
            .Type<EnumType<Visibility>>();

        descriptor.Field("color")
            .Resolve(ctx =>
            {
                var parent = ctx.Parent<Project>();
                return parent.Color?.ColorHex ?? string.Empty;
            })
        .Type<StringType>();
    }
}

public sealed class CreateProjectRequestType : InputObjectType<CreateProjectRequest>
{
    protected override void Configure(IInputObjectTypeDescriptor<CreateProjectRequest> descriptor)
    {
        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>();
        descriptor.Field(f => f.Description)
            .Type<StringType>();
        descriptor.Field(f => f.Color)
            .Type<NonNullType<StringType>>();
        descriptor.Field(f => f.Visibility)
            .Type<EnumType<Visibility>>();
    }
}

public sealed class UpdateProjectRequestType : InputObjectType<UpdateProjectRequest>
{
    protected override void Configure(IInputObjectTypeDescriptor<UpdateProjectRequest> descriptor)
    {
        descriptor.Field(f => f.Id)
            .Type<NonNullType<UuidType>>();
        descriptor.Field(f => f.Name)
            .Type<NonNullType<StringType>>();
        descriptor.Field(f => f.Description)
            .Type<StringType>();
        descriptor.Field(f => f.Color)
            .Type<StringType>();
        descriptor.Field(f => f.Visibility)
            .Type<EnumType<Visibility>>();
    }
}