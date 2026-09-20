namespace Ragkivio.Domain.Common;

public class Entity<T> where T : struct
{
    public T Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}

public class Entity : Entity<Guid>;