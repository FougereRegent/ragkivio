namespace Ragkivio.Persistence.Common.Entities;

internal class EntityWithTenancy : Entity
{
    public Guid TenantId { get; set; }
}