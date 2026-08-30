using System.ComponentModel.DataAnnotations;

namespace Ragkivio.Graphql.Options;

public class AuthOption
{
    public const string SectionName = "AUTH";

    [Required, ConfigurationKeyName("AUTHORIRY")]
    public string Authority { get; set; } = string.Empty;

    [Required, ConfigurationKeyName("AUDIENCE")]
    public string Audience { get; set; } = string.Empty;

    [Required, ConfigurationKeyName("AUTH_ID")]
    public string AuthId { get; set; } = string.Empty;

    [Required, ConfigurationKeyName("AUTH_DOMAIN")]
    public string AuthDomain { get; set; } = string.Empty;

    [Required, ConfigurationKeyName("AUTH_AUDIENCE")]
    public string AuthAudience { get; set; } = string.Empty;

}