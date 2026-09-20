using Ragkivio.Domain.User;
using Ragkivio.Persistence.Common;
using UserDomain = Ragkivio.Domain.User.User;
using UserPersistence = Ragkivio.Persistence.User.User;
using Microsoft.EntityFrameworkCore;

namespace Ragkivio.Persistence.User;

internal sealed class UserRepository : GenericRepository<UserPersistence, UserDomain>, IUserRepository
{
    public UserRepository(RagkivioContext dbContext) : base(dbContext) {}

    public async Task<UserDomain?> GetUserByAuthIdAsync(string authId, CancellationToken token = default)
    {
        var userPersistence = await _dbSet.FirstOrDefaultAsync(pre => pre.AuthId == authId, token);

        return userPersistence is null ? null : ToDomain(userPersistence);
    }

    public async Task<UserDomain> SaveAsync(UserDomain entity, string authId, CancellationToken token = default)
    {
        var userPersistence = UserMapper.ToPersistence(entity);
        userPersistence.AuthId = authId;
        await _context.Users.AddAsync(userPersistence, token);
        await _context.SaveChangesAsync(token);
        return ToDomain(userPersistence);
    }

    private protected override void HydratePersistenceEntity(UserDomain domainEntity, UserPersistence persistenceEntity)
    {
        UserMapper.Hydrate(domainEntity, persistenceEntity);
    }

    private protected override IQueryable<UserDomain> ProjectionToDomain(IQueryable<UserPersistence> collection)
    {
        return collection.ProjectToDomain();
    }

    private protected override UserDomain ToDomain(UserPersistence entity)
    {
        return UserMapper.ToDomain(entity);
    }

    private protected override UserPersistence ToPersistence(UserDomain entity)
    {
        return UserMapper.ToPersistence(entity);
    }
}