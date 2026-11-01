namespace Ragkivio.Graphql.Configuration.Presentation;

using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Configurations;
using Ragkivio.Domain.Common;
internal sealed class EntityTypeInterceptor : TypeInterceptor
{
    public override void OnBeforeRegisterDependencies(
        ITypeDiscoveryContext discoveryContext,
        TypeSystemConfiguration configuration)
    {
        if (configuration is not ObjectTypeConfiguration objectType)
        {
            return;
        }

        var inheritedFields = objectType.Fields
            .Where(field => field.Member?.DeclaringType == typeof(Entity))
            .ToArray();

        foreach (var field in inheritedFields)
        {
            objectType.Fields.Add(field);
        }
    }
}