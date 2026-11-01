namespace Ragkivio.Graphql.Configuration.Presentation;

using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Configurations;
using Ragkivio.Domain.Common;

/// <summary>
/// Prevents Hot Chocolate from inferring GraphQL fields for the members that
/// <see cref="AggregateRoot"/> exposes to the application layer
/// (for example <c>DomainEvents()</c>).
/// </summary>
internal sealed class AggregateRootTypeInterceptor : TypeInterceptor
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
            .Where(field => field.Member?.DeclaringType == typeof(AggregateRoot))
            .ToArray();

        foreach (var field in inheritedFields)
        {
            objectType.Fields.Remove(field);
        }
    }
}