using FluentResults;
using UserDomain = Ragkivio.Domain.User.User;
using Ragkivio.Application.User.Dto;

namespace Ragkivio.Application.User;

public interface IUserService {
    Task<Result<UserDomain>> CreateUserAsync(CreateUserDto createUser, CancellationToken token = default);
}