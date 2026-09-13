using Microsoft.EntityFrameworkCore;
using UserDomain = Ragkivio.Domain.User.User;
using UserPersistence = Ragkivio.Persistence.User.User;

namespace Ragkivio.Persistence.User;


public sealed class UserRepository : IUserRepository
{
    private readonly RagkivioContext _dbContext;

    public UserRepository(RagkivioContext dbContext) {
        this._dbContext = dbContext;
    }

    public async Task DeleteAsync(UserDomain entity, CancellationToken token = default)
    {
        await _dbContext.Users.Where(pre => pre.Id == entity.Id)
            .ExecuteDeleteAsync(token);
    }

    public async Task DeleteAsync(IEnumerable<UserDomain> entities, CancellationToken token = default)
    {
        var ids = entities.Select(pre => pre.Id)
            .ToList();

        await _dbContext.Users.Where(pre => ids.Contains(pre.Id))
            .ExecuteDeleteAsync(token);
    }

    public IQueryable<Domain.User.User> GetAll() => _dbContext.Users
        .Select(pre => new Domain.User.User())
        .AsQueryable();

    public async Task<Domain.User.User?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await _dbContext.Users
            .Select(pre => new UserDomain())
            .FirstOrDefaultAsync(pre => pre.Id == id, token);
    }

    public async Task<UserDomain> SaveAsync(UserDomain entity, CancellationToken token = default)
    {
        await _dbContext.Users.AddAsync(new UserPersistence(), token);
        return entity;
    }

    public async Task<IEnumerable<UserDomain>> SaveAsync(IEnumerable<UserDomain> entities, CancellationToken token = default)
    {
        var users = entities.Select(pre => new UserPersistence())
            .ToList();
        await _dbContext.Users.AddRangeAsync(users, token);
        return users.Select(pre => new)
    }

    public async Task UpdateAsync(UserDomain entity, CancellationToken token = default)
    {
        var user = new UserPersistence();
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task UpdateAsync(IEnumerable<UserDomain> entities, CancellationToken token = default)
    {
        var users = entities.Select(pre => new UserPersistence());
        _dbContext.Users.UpdateRange(users);
        await _dbContext.SaveChangesAsync(token);
    }
}