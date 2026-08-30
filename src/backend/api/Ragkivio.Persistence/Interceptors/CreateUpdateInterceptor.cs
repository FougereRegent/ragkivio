using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ragkivio.Persistence.Entities;

namespace Ragkivio.Persistence.Interceptors;

public class CreateUpdateInterceptor : ISaveChangesInterceptor
{
    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if(context is {} )
            this.ApplyCreateUpdate(context);
        return result;
    }

    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if(context is {} )
            this.ApplyCreateUpdate(context);
        return result;
    }

    private void ApplyCreateUpdate(DbContext context) {
        foreach(var entry in context.ChangeTracker.Entries<Entity>()) {
            switch(entry.State) {
                case EntityState.Modified:
                    entry.Entity.UpdatedAt =  DateTimeOffset.UtcNow;
                    break;
                case EntityState.Added:
                    entry.Entity.UpdatedAt =  DateTimeOffset.UtcNow;
                    entry.Entity.CreatedAt =  DateTimeOffset.UtcNow;
                    entry.Entity.DeletedAt =  null;
                    break;
                default:
                    break;
            }
        }
    }
}