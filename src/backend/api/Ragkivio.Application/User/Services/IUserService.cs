using FluentResults;
using UserDomain = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User;

public interface IUserService {
    Task<Result<UserDomain>> CreateUserAsync(CancellationToken token = default);
}