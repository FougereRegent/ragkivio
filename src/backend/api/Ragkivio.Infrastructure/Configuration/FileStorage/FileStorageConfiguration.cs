using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ragkivio.Infrastructure.Common;
using Ragkivio.Infrastructure.FileStorage;

namespace Ragkivio.Infrastructure.Configuration.FileStorage;

public static class FileStorageConfiguration
{
    extension(IServiceCollection serviceCollection)
    {
        public IServiceCollection AddFileStorage()
        {
            serviceCollection.AddOptions<FileStorageOption>()
                .BindConfiguration(FileStorageOption.CONFIGURATION_KEY)
                .ValidateOnStart();
            
            serviceCollection.AddSingleton<IAmazonS3>(serviceProvider =>
            {
                var fileStorageOption = serviceProvider.GetRequiredService<IOptions<FileStorageOption>>()
                    .Value;
                var config = new AmazonS3Config
                {
                    IgnoreConfiguredEndpointUrls = true,
                    ServiceURL = fileStorageOption.ServerUrl,
                    ForcePathStyle = true,
                    AuthenticationRegion = "us-east-1",
                    UseAccelerateEndpoint = false,
                    Timeout = TimeSpan.FromSeconds(30),
                };

                return new AmazonS3Client(
                        new BasicAWSCredentials(accessKey: fileStorageOption.AccessKey, secretKey: fileStorageOption.SecretKey),
                        config
                        );
            });

            serviceCollection.AddKeyedTransient<IInit, InitFileStorage>(InitName.InitStorage);
            return serviceCollection;
        }
    }
}