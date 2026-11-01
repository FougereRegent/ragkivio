

namespace Ragkivio.Graphql.Types;

using Ragkivio.Graphql.Types.Project;
using User;

public partial class Query
{
}

public class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Authorize();
        descriptor.Me()
            .Projects();
    }
}