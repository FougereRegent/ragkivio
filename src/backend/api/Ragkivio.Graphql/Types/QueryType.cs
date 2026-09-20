using Ragkivio.Graphql.Middleware;


namespace Ragkivio.Graphql.Types;
using User;

public partial class Query
{
}

public class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Authorize();
        descriptor.Me();
    }
}