using DomainUser = Ragkivio.Domain.User.User;

namespace Ragkivio.Application.User.Services;


public interface IUserProvider
{
    DomainUser CurrentUser { get; }
    Guid UserId {get;}

    void SetCurrentUser(DomainUser user);
}