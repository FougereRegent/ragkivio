namespace Ragkivio.Domain.Common;

public interface IGenericRepository<T> where T : Entity
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(Guid id, CancellationToken token = default);
    Task<T?> SaveAsync(T entity, CancellationToken token = default);
    Task<IEnumerable<T>> SaveAsync(IEnumerable<T> entities, CancellationToken token = default);
    Task UpdateAsync(T entity, CancellationToken token = default);
    Task UpdateAsync(IEnumerable<T> entities, CancellationToken token = default);
    Task DeleteAsync(T entity, CancellationToken token = default);
    Task DeleteAsync(IEnumerable<T> entities, CancellationToken token = default);

}