using Microsoft.EntityFrameworkCore;
using Ragkivio.Persistence.Common.Entities;

namespace Ragkivio.Persistence.Common;

internal abstract class GenericRepository<T_IN, T_OUT> : Domain.Common.IGenericRepository<T_OUT> where T_IN : Entity where T_OUT : Domain.Common.Entity {

    protected readonly DbSet<T_IN> _dbSet;
    protected readonly RagkivioContext _context;

    private protected abstract IQueryable<T_OUT> ProjectionToDomain(IQueryable<T_IN> collection);

    private protected abstract T_IN ToPersistence(T_OUT entity);
    private protected abstract T_OUT ToDomain(T_IN entity);
    private protected abstract void HydratePersistenceEntity(T_OUT domainEntity, T_IN persistenceEntity);

    private protected IEnumerable<T_IN> ToPersistence(IEnumerable<T_OUT> entities) =>
        entities.Select(pre => ToPersistence(pre));

    private protected IEnumerable<T_OUT> ToDomain(IEnumerable<T_IN> entities) =>
        entities.Select(pre => ToDomain(pre));

    public GenericRepository(RagkivioContext context) {
        this._context = context;
        this._dbSet = context.Set<T_IN>();
    }

    public async Task DeleteAsync(T_OUT entity, CancellationToken token = default)
    {
        var persistenceEntity = ToPersistence(entity);
        await _dbSet.Where(pre => pre.Id == persistenceEntity.Id)
            .ExecuteDeleteAsync(token);
        await _context.SaveChangesAsync(token);
    }

    public async Task DeleteAsync(IEnumerable<T_OUT> entities, CancellationToken token = default)
    {
        var persistenceEntities = ToPersistence(entities)
            .Select(pre => pre.Id)
            .ToHashSet();

        await _dbSet.Where(pre => persistenceEntities.Contains(pre.Id))
            .ExecuteDeleteAsync(token);
        await _context.SaveChangesAsync(token);
    }

    public IQueryable<T_OUT> GetAll() =>
        ProjectionToDomain(_dbSet.AsQueryable());

    public async Task<T_OUT?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        var result = await _dbSet.FirstOrDefaultAsync(pre => pre.Id == id, token);
        return result is not null ? ToDomain(result) : null;
    }

    public async Task<T_OUT?> SaveAsync(T_OUT entity, CancellationToken token = default)
    {
        var persistenceEntity = ToPersistence(entity);
        await _dbSet.AddAsync(persistenceEntity, token);
        await _context.SaveChangesAsync(token);
        return ToDomain(persistenceEntity);
    }

    public virtual async Task<IEnumerable<T_OUT>> SaveAsync(IEnumerable<T_OUT> entities, CancellationToken token = default)
    {
        var persistenceEntities = ToPersistence(entities)
            .ToHashSet();
        await _dbSet.AddRangeAsync(persistenceEntities, token);
        await _context.SaveChangesAsync(token);
        return ToDomain(persistenceEntities);
    }

    public async Task UpdateAsync(T_OUT entity, CancellationToken token = default)
    {
        var entityPersistence = await _dbSet.FindAsync(entity.Id);
        ArgumentNullException.ThrowIfNull(entityPersistence);

        HydratePersistenceEntity(entity, entityPersistence);
        _dbSet.Update(entityPersistence);
        await _context.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<T_OUT> entities, CancellationToken token = default)
    {
        foreach (var entity in entities)
        {
            var entityPersistence = await _dbSet.FindAsync(entity.Id, token);
            ArgumentNullException.ThrowIfNull(entityPersistence);
            HydratePersistenceEntity(entity, entityPersistence);
            
            _dbSet.Update(entityPersistence);
        }
        await _context.SaveChangesAsync(token);
    }
}