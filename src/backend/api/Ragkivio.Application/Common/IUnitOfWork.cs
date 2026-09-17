namespace Ragkivio.Application.Common;

public interface IUnitOfWork
{
    Task ExecuteAsync(Action act, CancellationToken cancellationToken = default);
    Task ExecuteAsync(Func<Task> act, CancellationToken cancellationToken = default);
    Task<T> ExecuteAsync<T>(Func<Task<T>> act, CancellationToken cancellationToken = default);
}