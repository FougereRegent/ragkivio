using DomainUser = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User.Services;


public sealed class UserProvider : IUserProvider
{
    private static readonly AsyncLocal<DomainUser> AsyncLocalCurrentUser = new AsyncLocal<DomainUser>();
    public DomainUser CurrentUser => AsyncLocalCurrentUser.Value ?? new DomainUser();
    public Guid UserId => CurrentUser?.Id ?? Guid.Empty;

    public void SetCurrentUser(DomainUser user)
    {
        ArgumentNullException.ThrowIfNull(user);
        AsyncLocalCurrentUser.Value = user;
    }
}