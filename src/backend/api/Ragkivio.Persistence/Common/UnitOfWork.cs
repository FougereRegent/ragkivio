namespace Ragkivio.Persistence.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ragkivio.Application.Common;
using Ragkivio.Application.Configuration;
using Ragkivio.Domain.Common;
public sealed class UnitOfWork(IDomainEventDispatcher eventDispatcher, IDomainEventCollector domainEventCollector,
        RagkivioContext dbContext) : IUnitOfWork
{
    public async Task ExecuteAsync(Action act, CancellationToken cancellationToken = default)

    {
        await ExecuteAsync(() =>
        {
            act();
            return Task.CompletedTask;
        }, cancellationToken);
    }

    public async Task ExecuteAsync(Func<Task> act, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync(async () =>
        {
            await act();
            return true;
        }, cancellationToken);
    }


    public async Task<T> ExecuteAsync<T>(Func<Task<T>> act, CancellationToken cancellationToken = default)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        T result;
        try
        {
            // Le dispatch est volontairement HORS de l'execution strategy :
            // un retry ne doit pas ré-exécuter les handlers.
            result = await strategy.ExecuteAsync(
                    state: (dbContext, act),
                    operation: async (state, ct) =>
                    {
                        await using var transaction = await state.dbContext.Database.BeginTransactionAsync(ct);
                        try
                        {
                            var value = await state.act();
                            await state.dbContext.SaveChangesAsync(ct);
                            await transaction.CommitAsync(ct);
                            return value;
                        }
                        catch
                        {
                            await transaction.RollbackAsync(ct);
                            throw;
                        }
                    },
                    verifySucceeded: null);
        }
        catch
        {
            // Transaction annulée : les événements ne doivent jamais partir.
            domainEventCollector.Discard();
            throw;
        }

        var events = domainEventCollector.Drain();
        if (events.Count > 0)
            await eventDispatcher.DispatchAsync(events, cancellationToken);

        return result;
    }

    public Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken token = default)
    {
        return dbContext.Database.BeginTransactionAsync(token);
    }
}