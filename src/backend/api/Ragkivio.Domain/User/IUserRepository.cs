using Ragkivio.Domain.Common;

namespace Ragkivio.Domain.User;

public interface IUserRepository : IGenericRepository<User> {
    Task<User?> GetUserByAuthIdAsync(string authId, CancellationToken token = default);
    Task<User> SaveAsync(User user, string authId, CancellationToken token = default);
}