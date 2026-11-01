using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ragkivio.Persistence.Common.Entities;

namespace Ragkivio.Persistence.Common.Interceptors;

public sealed class TenancyInterceptor : ISaveChangesInterceptor {

    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if(context is {} )
            this.ApplyTenancy(context);
        return result;
    }

    public InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var context = eventData.Context;
        if(context is {} )
            this.ApplyTenancy(context);
        return result;
    }

    private void ApplyTenancy(DbContext context) {
        foreach(var entry in context.ChangeTracker.Entries<EntityWithTenancy>()) {
            if(entry.State != EntityState.Added)
                continue;

            var myDbContext = (RagkivioContext)context;
            entry.Entity.TenantId = myDbContext.TenantId;
        }
    }
}