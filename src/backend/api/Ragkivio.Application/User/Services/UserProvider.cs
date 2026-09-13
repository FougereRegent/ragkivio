using DomainUser = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User.Services;


public sealed class UserProvider : IUserProvider
{
    public DomainUser CurrentUser {get; private set;} = null!;

    public Guid UserId => CurrentUser?.Id ?? Guid.Empty;

    public void SetCurrentUser(DomainUser user)
    {
        ArgumentNullException.ThrowIfNull(user);
        this.CurrentUser = user;
    }
}