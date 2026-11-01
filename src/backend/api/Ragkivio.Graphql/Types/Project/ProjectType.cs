namespace Ragkivio.Graphql.Types.Project;

using Ragkivio.Domain.Project;

public sealed class ProjectType : ObjectType<Project>
{
    protected override void Configure(IObjectTypeDescriptor<Project> descriptor)
    {
        base.Configure(descriptor);
    }
}