namespace Ragkivio.Domain.Common;

public class Entity<T> where T : struct
{
    public T Id { get; protected set; }
    public DateTimeOffset CreatedAt { get; protected set; }
    public DateTimeOffset UpdatedAt { get; protected set; }
}

public class Entity : Entity<Guid>;