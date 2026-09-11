namespace Ragkivio.Domain.User;

public class User : Common.Entity
{
    public string Email { get; } = string.Empty;
    public string FirstName { get; } = string.Empty;
    public string LastName { get; } = string.Empty;
    public string PhoneNumber { get; } = string.Empty;
    public DateOnly BirthDate { get; } = new DateOnly();

    public Config? Config {get;} = null;
}