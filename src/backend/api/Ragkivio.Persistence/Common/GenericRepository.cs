using Ragkivio.Domain.Common;

namespace Ragkivio.Persistence.Common;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
{
    protected readonly RagkivioContext _dbContext;

    public GenericRepository(RagkivioContext dbContext) {
        ArgumentNullException.ThrowIfNull(dbContext);

        _dbContext = dbContext;
    }

    public abstract Task DeleteAsync(T entity, CancellationToken token = default);

    public abstract Task DeleteAsync(IEnumerable<T> entities, CancellationToken token = default);

    public abstract IQueryable<T> GetAll();

    public abstract Task<T?> GetByIdAsync(Guid id);

    public abstract Task<T?> SaveAsync(T entity, CancellationToken token = default)

    public abstract Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities, CancellationToken token = default)

    public abstract Task UpdateAsync(T entity, CancellationToken token = default)

    public abstract Task UpdateAsync(IEnumerable<T> entities, CancellationToken token = default)
}