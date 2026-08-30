using System.ComponentModel.DataAnnotations;

namespace Ragkivio.Graphql.Options;

public class DatabaseOption
{
    public const string SectionName = "DB";

    [Required, ConfigurationKeyName("CONNECTION_STRING")]
    public string ConnectionString { get; set; } = string.Empty;
}