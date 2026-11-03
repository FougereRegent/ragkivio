using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Ragkivio.Application.User.Services;
using Ragkivio.Persistence.Common.Entities;
using Ragkivio.Persistence.Common.Interceptors;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Ragkivio.Persistence;

public class RagkivioContext : DbContext
{
    internal const string TENANCY_QUERY_FILTER = "query-flter.tenancy";
    internal const string DELETE_QUERY_FILTER = "delete-flter.tenancy";
        
    private readonly IUserProvider userProvider;

    internal DbSet<Persistence.User.User> Users { get; set; }

    internal Guid TenantId => userProvider?.UserId ?? Guid.Empty;

    public RagkivioContext(DbContextOptions<RagkivioContext> options, IUserProvider userProvider) : base(options)
    {
        this.userProvider = userProvider;
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
                new TenancyInterceptor(),
                new CreateUpdateInterceptor(),
        ]);

        base.OnConfiguring(optionsBuilder);
    }


    private void ConfigureEntities(ModelBuilder modelBuilder)
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
                entity.FindProperty(nameof(Entity.DeletedAt))?
                    .SetColumnName("deleted_at");
                entity.FindProperty(nameof(Entity.IsDelete))?
                    .SetColumnName("is_delete");
             
                var parameter = Expression.Parameter(entity.ClrType, "e");
                var propertyExpression = Expression.Property(parameter, nameof(Entity.IsDelete));
                var isDeleteLambda = Expression.Lambda(Expression.Not(propertyExpression), parameter);
                
                entity.SetQueryFilter(DELETE_QUERY_FILTER, isDeleteLambda);
            }

            if (typeof(EntityWithTenancy).IsAssignableFrom(entity.ClrType))
            {
                entity.FindProperty(nameof(EntityWithTenancy.TenantId))?
                    .SetColumnName("tenant_id");
                entity.FindProperty(nameof(EntityWithTenancy.TenantId))?
                    .SetColumnOrder(2);

                var parameter = Expression.Parameter(entity.ClrType, "e");
                var propertyExpression = Expression.Property(parameter, nameof(EntityWithTenancy.TenantId));
                var tenantIdExpression = Expression.Property(Expression.Constant(this), nameof(TenantId));
                var equalExpression = Expression.Equal(propertyExpression, tenantIdExpression);
                var tenantIdFilter = Expression.Lambda(equalExpression, parameter);

                entity.SetQueryFilter(TENANCY_QUERY_FILTER, tenantIdFilter);
            }
        }
    }
}