namespace Ragkivio.Persistence.Entities;

internal class EntityWithTenancy : Entity
{
    public Guid TenantId { get; set; }
}