using System.ComponentModel.DataAnnotations;

namespace Ragkivio.Graphql.Options;

public class DatabaseOption
{
    public const string SectionName = "ConnectionStrings";

    [Required, ConfigurationKeyName("ragkivio")]
    public string ConnectionString { get; set; } = string.Empty;
}