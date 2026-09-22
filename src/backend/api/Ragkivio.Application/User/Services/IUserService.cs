
using FluentResults;

namespace Ragkivio.Application.User;

public interface IUserService {
    Result<bool> UserRegistrationIsCompleted();
}