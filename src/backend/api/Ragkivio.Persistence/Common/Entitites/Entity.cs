using System.Collections;

namespace Ragkivio.Persistence.Common.Entities;

internal class Entity : IEqualityComparer
{
    public Guid Id { get; set; }
    public bool IsDelete { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public DateTimeOffset? DeletedAt { get; set; } = null;

    public new bool Equals(object? x, object? y)
    {
        var entityA = x as Entity;
        var entityB = y as Entity;

        if(entityA == null || entityB == null) 
            return false; 

        return entityA.Id == entityB.Id;
    }

    public int GetHashCode(object obj)
    {
        var entity = obj as Entity;
        if(entity is null)
            return obj.GetHashCode();
        return entity.Id.GetHashCode();
    }
}