using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ragkivio.Persistence.Entities;

namespace Ragkivio.Persistence.Interceptors;


public class SoftDeleteInterceptor : ISaveChangesInterceptor {
    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if(context is {} )
            this.ApplySoftDelete(context);
        return result;
    }

    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if(context is {} )
            this.ApplySoftDelete(context);
        return result;
    }

    private void ApplySoftDelete(DbContext context) {
        foreach(var entry in context.ChangeTracker.Entries<Entity>()) {
            if(entry.State != EntityState.Deleted)
                continue;

            entry.State = EntityState.Modified;
            entry.Entity.IsDelete = true;
        }
    }
}