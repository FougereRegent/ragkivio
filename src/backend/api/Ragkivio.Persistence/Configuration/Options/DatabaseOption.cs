using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Ragkivio.Persistence.Common.Options;

public class DatabaseOption
{
    public const string SectionName = "ConnectionStrings";

    [Required, ConfigurationKeyName("ragkivio")]
    public string ConnectionString { get; set; } = string.Empty;
}