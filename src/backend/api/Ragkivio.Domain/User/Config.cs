namespace Ragkivio.Domain.User;

public class Config : Common.Entity
{
    public string TimeZone { get; } = string.Empty;
    public string Locale { get; } = string.Empty;
}