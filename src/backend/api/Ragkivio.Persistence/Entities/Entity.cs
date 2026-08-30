namespace Ragkivio.Persistence.Entities;

internal class Entity
{
    public Guid Id { get; set; }
    public bool IsDelete { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; } = null;
}