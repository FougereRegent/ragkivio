namespace Ragkivio.Domain.Common;

public class Entity<T> where T : struct
{
    public T Id { get; protected set; }
}

public class Entity : Entity<Guid>;