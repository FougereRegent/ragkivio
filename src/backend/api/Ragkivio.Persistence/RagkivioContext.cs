using Microsoft.EntityFrameworkCore;
using Ragkivio.Persistence.Common.Entities;
using Ragkivio.Persistence.Common.Interceptors;

namespace Ragkivio.Persistence;

public class RagkivioContext : DbContext
{

    internal DbSet<Persistence.User.User> Users { get; set; }

    public RagkivioContext(DbContextOptions<RagkivioContext> options) : base(options)
    {
    }

    protected RagkivioContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RagkivioContext).Assembly);
        ConfigureEntities(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql()
            .UseLowerCaseNamingConvention();

        optionsBuilder.AddInterceptors([
                new SoftDeleteInterceptor(),
                new CreateUpdateInterceptor(),
        ]);

        base.OnConfiguring(optionsBuilder);
    }


    private static void ConfigureEntities(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(entity.ClrType))
            {
                var idProperty = entity.FindProperty(nameof(Entity.Id));
                idProperty?.SetColumnName("id");
                idProperty?.SetColumnOrder(1);
                idProperty?.SetDefaultValueSql("uuidv7()");

                entity.FindProperty(nameof(Entity.CreatedAt))?
                    .SetColumnName("created_at");
                entity.FindProperty(nameof(Entity.UpdatedAt))?
                    .SetColumnName("updated_at");
            }

            if (typeof(EntityWithTenancy).IsAssignableFrom(entity.ClrType))
            {
                entity.FindProperty(nameof(EntityWithTenancy.TenantId))?
                    .SetColumnName("tenant_id");
                entity.FindProperty(nameof(EntityWithTenancy.TenantId))?
                    .SetColumnOrder(2);
            }
        }
    }
}