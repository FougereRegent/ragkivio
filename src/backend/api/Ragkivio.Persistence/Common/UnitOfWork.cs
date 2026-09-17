namespace Ragkivio.Persistence.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ragkivio.Application.Common;

public sealed class UnitOfWork(RagkivioContext dbContext) : IUnitOfWork
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

        return await strategy.ExecuteAsync(
                state: (dbContext, act),
                operation: async (state, ct) =>
                {
                    await using var transaction = await state.dbContext.Database.BeginTransactionAsync(ct);

                    try
                    {
                        var result = await state.act();
                        await state.dbContext.SaveChangesAsync(ct);
                        await transaction.CommitAsync(ct);
                        return result;
                    }
                    catch
                    {
                        await transaction.RollbackAsync(ct);
                        throw;
                    }
                },
                verifySucceeded: null);
    }

    public Task<IDbContextTransaction> CreateTransactionAsync(CancellationToken token = default)
    {
        return dbContext.Database.BeginTransactionAsync(token);
    }
}