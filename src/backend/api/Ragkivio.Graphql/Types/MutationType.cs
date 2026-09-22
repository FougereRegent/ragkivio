using Ragkivio.Graphql.Types.User;

namespace Ragkivio.Graphql.Types;

public partial class Mutation;

public class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Types.Mutation> descriptor)
    {
        descriptor.Authorize();
        descriptor.RegisterUser();
    }
}