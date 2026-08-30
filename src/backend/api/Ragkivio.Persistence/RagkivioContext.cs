using Microsoft.EntityFrameworkCore;
using Ragkivio.Persistence.Entities;

namespace Ragkivio.Persistence.Configuration;

public class RagkivioContext : DbContext
{
    public RagkivioContext(DbContextOptions options) : base(options)
    {
    }

    protected RagkivioContext()
    {
    }

    public override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RagkivioContext).Assembly);
    }

    public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }


    private static void ConfigureEntities(this ModelBuilder modelBuilder) {
        foreach(var entity in modelBuilder.Model.GetEntityTypes()) {
            if(typeof(Entity).IsAssignableFrom(entity.ClrType)) {
                var idProperty = entity.FindProperty(nameof(Entity.Id));
                idProperty?.SetColumnName("id");
                idProperty?.SetColumnOrder(1);
                idProperty?.SetDefaultValueSql("uuidv7()");

                entity.FindProperty(nameof(Entity.CreatedAt))?
                    .SetColumnName("created_at");
                entity.FindProperty(nameof(Entity.UpdatedAt))?
                    .SetColumnName("updated_at");
            }

            if(typeof(EntityWithTenancy).IsAssignableFrom(entity.ClrType)) {
                entity.FindProperty(nameof(EntityWithTenancy.TenantId))?
                    .SetColumnName("tenant_id");
                entity.FindProperty(nameof(EntityWithTenancy.TenantId))?
                    .SetColumnOrder(2);
            }
        }
    }
}