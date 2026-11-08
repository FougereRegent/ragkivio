namespace Ragkivio.Infrastructure.Configuration.FileStorage;

using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

internal class FileStorageOption
{
    internal const string CONFIGURATION_KEY = "FILE_STORAGE";

    [Required, ConfigurationKeyName("SERVER_URL")]
    public string ServerUrl { get; set; } = string.Empty;

    [Required, ConfigurationKeyName("ACCESS_KEY")]
    public string AccessKey { get; set; } = string.Empty;

    [Required, ConfigurationKeyName("SECRET_KEY")]
    public string SecretKey { get; set; } = string.Empty;

    [Required, ConfigurationKeyName("BUCKET_NAME")]
    public string BucketName { get; set; } = string.Empty;
}